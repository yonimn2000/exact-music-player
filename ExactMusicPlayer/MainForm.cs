using AxWMPLib;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using YonatanMankovich.PlaylistPlanner;

namespace YonatanMankovich.ExactMusicPlayer
{
    public partial class MainForm : Form
    {
        private Planner Planner { get; set; } = new Planner();
        private Playlist Playlist { get; set; }
        private MusicFile CurrentMusicFile { get; set; }

        public MainForm()
        {
            InitializeComponent();
            MediaPlayer.settings.volume = 100;
        }

        private TimeSpan GetRemainingDuration() => PlayUntilDtp.Value - DateTime.Now;

        private void MainForm_Load(object sender, EventArgs e)
        {
            PlayUntilDtp.Value = DateTime.Now.AddHours(1);
            if(!string.IsNullOrWhiteSpace(Properties.Settings.Default.OpenFolder))
            {
                folderDialog.SelectedPath = Properties.Settings.Default.OpenFolder;
            }
        }

        double newPosition = 0;
        bool mediaJustEnded = false;
        private void MediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 1:    // Stopped
                case 2:    // Paused
                    {
                        newPosition = MediaPlayer.Ctlcontrols.currentPosition;
                    }
                    break;
                case 3:    // Playing
                    if (!mediaJustEnded)
                    {
                        UpdatePlaylist(true);
                        mediaJustEnded = false;
                    }
                    break;
                case 8:
                    mediaJustEnded = true;
                    PlayNextSong();
                    break; // MediaEnded
            }
        }

        private void PlayNextSong()
        {
            if (Playlist.Size > 0)
            {
                CurrentMusicFile = Playlist.DequeueMusicFile();
                MediaPlayer.URL = CurrentMusicFile.Path;
                PlayingUntilLbl.Text = "Playing until: " + (DateTime.Now + Playlist.Duration + CurrentMusicFile.Duration) + $" ({Playlist.Size} files)";
                new System.Threading.Timer((s) => { MediaPlayer.Ctlcontrols.play(); }, null, 10, Timeout.Infinite);
            }
        }

        private void PlayUntilDtp_ValueChanged(object sender, EventArgs e)
        {
            UpdatePlaylist(true);
        }

        private void UpdatePlaylist(bool considerCurrentPosition)
        {
            TimeSpan remainingPlayback = (considerCurrentPosition && CurrentMusicFile != null ?
                            CurrentMusicFile.Duration - TimeSpan.FromSeconds(newPosition) : TimeSpan.Zero);
            Playlist = Planner.GetClosestPlaylistOfDuration(GetRemainingDuration() - remainingPlayback);
            PlayingUntilLbl.Text = "Playing until: " + (DateTime.Now + Playlist.Duration + remainingPlayback) + $" ({Playlist.Size} files)";
        }

        private void SkipBtn_Click(object sender, EventArgs e)
        {
            UpdatePlaylist(false);
            PlayNextSong();
        }

        private void MediaPlayer_PositionChange(object sender, _WMPOCXEvents_PositionChangeEvent e)
        {
            newPosition = e.newPosition;
            UpdatePlaylist(true);
        }

        private void SelectFolderLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = folderDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                SelectFolderLink.Text = "Loading files...";
                SelectFolderLink.Enabled = false;
                Planner.AddMusicFilesFromDirectory(folderDialog.SelectedPath);
                SelectFolderLink.Text = folderDialog.SelectedPath;
                SelectFolderLink.Enabled = true;
                UpdatePlaylist(false);
                CurrentMusicFile = Playlist.DequeueMusicFile();
                MediaPlayer.URL = CurrentMusicFile.Path;
                Properties.Settings.Default.OpenFolder = folderDialog.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}