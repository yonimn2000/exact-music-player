﻿namespace YonatanMankovich.ExactMusicPlayer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.PlayUntilDtp = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SkipBtn = new System.Windows.Forms.Button();
            this.SelectFolderLink = new System.Windows.Forms.LinkLabel();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.PlayingUntilLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(13, 68);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(247, 62);
            this.MediaPlayer.TabIndex = 0;
            this.MediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.MediaPlayer_PlayStateChange);
            this.MediaPlayer.PositionChange += new AxWMPLib._WMPOCXEvents_PositionChangeEventHandler(this.MediaPlayer_PositionChange);
            // 
            // PlayUntilDtp
            // 
            this.PlayUntilDtp.CustomFormat = "MM/dd/yyyy H:mm:ss";
            this.PlayUntilDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.PlayUntilDtp.Location = new System.Drawing.Point(59, 27);
            this.PlayUntilDtp.Name = "PlayUntilDtp";
            this.PlayUntilDtp.Size = new System.Drawing.Size(159, 20);
            this.PlayUntilDtp.TabIndex = 1;
            this.PlayUntilDtp.ValueChanged += new System.EventHandler(this.PlayUntilDtp_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Play until";
            // 
            // SkipBtn
            // 
            this.SkipBtn.Location = new System.Drawing.Point(224, 26);
            this.SkipBtn.Name = "SkipBtn";
            this.SkipBtn.Size = new System.Drawing.Size(37, 23);
            this.SkipBtn.TabIndex = 3;
            this.SkipBtn.Text = "Skip";
            this.SkipBtn.UseVisualStyleBackColor = true;
            this.SkipBtn.Click += new System.EventHandler(this.SkipBtn_Click);
            // 
            // SelectFolderLink
            // 
            this.SelectFolderLink.AutoSize = true;
            this.SelectFolderLink.Location = new System.Drawing.Point(10, 9);
            this.SelectFolderLink.Name = "SelectFolderLink";
            this.SelectFolderLink.Size = new System.Drawing.Size(66, 13);
            this.SelectFolderLink.TabIndex = 4;
            this.SelectFolderLink.TabStop = true;
            this.SelectFolderLink.Text = "Select folder";
            this.SelectFolderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SelectFolderLink_LinkClicked);
            // 
            // folderDialog
            // 
            this.folderDialog.Description = "Select a folder with music files:";
            this.folderDialog.ShowNewFolderButton = false;
            // 
            // PlayingUntilLbl
            // 
            this.PlayingUntilLbl.AutoSize = true;
            this.PlayingUntilLbl.Location = new System.Drawing.Point(10, 50);
            this.PlayingUntilLbl.Name = "PlayingUntilLbl";
            this.PlayingUntilLbl.Size = new System.Drawing.Size(156, 13);
            this.PlayingUntilLbl.TabIndex = 5;
            this.PlayingUntilLbl.Text = "Playing until: -----------------------------";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 141);
            this.Controls.Add(this.PlayingUntilLbl);
            this.Controls.Add(this.SelectFolderLink);
            this.Controls.Add(this.SkipBtn);
            this.Controls.Add(this.PlayUntilDtp);
            this.Controls.Add(this.MediaPlayer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Exact Music Player";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
        private System.Windows.Forms.DateTimePicker PlayUntilDtp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SkipBtn;
        private System.Windows.Forms.LinkLabel SelectFolderLink;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Label PlayingUntilLbl;
    }
}
