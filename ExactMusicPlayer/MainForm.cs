using AxWMPLib;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using YonatanMankovich.PlaylistPlanner;

namespace YonatanMankovich.ExactMusicPlayer
{
    public partial class MainForm : Form
    {
        private Planner Planner { get; set; }
        private Playlist Playlist { get; set; }
        private MusicFile CurrentMusicFile { get; set; }

        public MainForm()
        {
            InitializeComponent();
            MediaPlayer.settings.volume = 100;

            DateTimeFormatInfo dateTimeFormat = CultureInfo.CurrentUICulture.DateTimeFormat;
            PlayUntilDtp.CustomFormat = dateTimeFormat.ShortDatePattern + " " + dateTimeFormat.LongTimePattern;

            PlayUntilDtp.Value = DateTime.Now.AddHours(1);
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.OpenFolder))
                folderDialog.SelectedPath = Properties.Settings.Default.OpenFolder;
        }

        double newPosition = 0;
        bool updatePlaylistOnPlaying = false;
        private void MediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 1:    // Stopped
                case 2:    // Paused
                    {
                        Console.WriteLine("Stopped or paused.");
                        newPosition = MediaPlayer.Ctlcontrols.currentPosition;
                        updatePlaylistOnPlaying = true;
                    }
                    break;
                case 3:    // Playing
                    {
                        Console.WriteLine("Playing.");
                        if (updatePlaylistOnPlaying)
                        {
                            UpdatePlaylist(true);
                            updatePlaylistOnPlaying = false;
                        }
                    }
                    break;
                case 8: // MediaEnded
                    {
                        Console.WriteLine("Media ended.");
                        updatePlaylistOnPlaying = false;
                        PlayNextSong();
                    }
                    break;
            }
        }

        private void PlayNextSong()
        {
            if (Playlist.Size > 0)
            {
                Console.WriteLine("Playing next song");
                CurrentMusicFile = Playlist.DequeueMusicFile();
                MediaPlayer.URL = CurrentMusicFile.Path;
                PlayingUntilLbl.Text = "Playing until: " + (DateTime.Now + Playlist.Duration + CurrentMusicFile.Duration) + $" ({Playlist.Size} files)";
                new System.Threading.Timer((s) => { MediaPlayer.Ctlcontrols.play(); }, null, 10, Timeout.Infinite);
            }
            if (Playlist.Size == 0)
                SkipBtn.Enabled = false;
        }

        private void PlayUntilDtp_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Playing until changed.");
            if (Planner != null)
                UpdatePlaylist(true);
        }

        private void UpdatePlaylist(bool considerCurrentPosition)
        {
            Console.WriteLine("Updating playlist. Consider current position: " + considerCurrentPosition);
            TimeSpan remainingPlayback = (considerCurrentPosition && CurrentMusicFile != null ?
                            CurrentMusicFile.Duration - TimeSpan.FromSeconds(newPosition) : TimeSpan.Zero);
            Playlist = Planner.GetClosestPlaylistOfDuration(PlayUntilDtp.Value - DateTime.Now - remainingPlayback);
            PlayingUntilLbl.Text = "Playing until: " + (DateTime.Now + Playlist.Duration + remainingPlayback) + $" ({Playlist.Size} files)";
            SkipBtn.Enabled = Playlist.Size > 0;
        }

        private void SkipBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Skipping.");
            UpdatePlaylist(false);
            PlayNextSong();
        }

        private void MediaPlayer_PositionChange(object sender, _WMPOCXEvents_PositionChangeEvent e)
        {
            Console.WriteLine("Position changed.");
            newPosition = e.newPosition;
            UpdatePlaylist(true);
        }

        private void SelectFolderLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Console.WriteLine("Selecting folder.");
            DialogResult dialogResult = folderDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                Console.WriteLine("Folder selected.");
                SelectFolderLink.Enabled = false;
                Planner = new Planner();
                Planner.ReportProgressDelegate += PlannerProgressReport;
                MusicLoaderBw.RunWorkerAsync();
            }
        }

        private void PlannerProgressReport(int fileIndex, int totalFiles)
        {
            Invoke((MethodInvoker)delegate
            {
                SelectFolderLink.Text = $"Loading files {fileIndex + 1}/{totalFiles} ({Math.Round(100.0 * (fileIndex + 1) / totalFiles)}%)";
            });
        }

        private void MusicLoaderBw_DoWork(object sender, DoWorkEventArgs e)
        {
            Planner.AddMusicFilesFromDirectory(folderDialog.SelectedPath);
        }

        private void MusicLoaderBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                SelectFolderLink.Text = folderDialog.SelectedPath;
                SelectFolderLink.Enabled = true;
                UpdatePlaylist(false);
                if (Playlist.Size > 0)
                {
                    CurrentMusicFile = Playlist.DequeueMusicFile();
                    MediaPlayer.URL = CurrentMusicFile.Path;
                    PlayingUntilLbl.Enabled = true;
                    SkipBtn.Enabled = true;
                    Properties.Settings.Default.OpenFolder = folderDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            });
        }
    }
}