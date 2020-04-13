namespace BackgroundKeyListener
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lboxAddedKeys = new System.Windows.Forms.ListBox();
            this.btnAddEvent = new System.Windows.Forms.Button();
            this.btnDeleteEvent = new System.Windows.Forms.Button();
            this.btnEditEvent = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnChooseSound = new System.Windows.Forms.Button();
            this.tbSoundPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseWavSoundFile = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.gbActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(93, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(212, 17);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(47, 13);
            this.lbStatus.TabIndex = 3;
            this.lbStatus.Text = "Stopped";
            // 
            // lboxAddedKeys
            // 
            this.lboxAddedKeys.FormattingEnabled = true;
            this.lboxAddedKeys.Items.AddRange(new object[] {
            "(empty)"});
            this.lboxAddedKeys.Location = new System.Drawing.Point(13, 42);
            this.lboxAddedKeys.Name = "lboxAddedKeys";
            this.lboxAddedKeys.Size = new System.Drawing.Size(155, 147);
            this.lboxAddedKeys.TabIndex = 4;
            this.lboxAddedKeys.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.Location = new System.Drawing.Point(6, 19);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Size = new System.Drawing.Size(75, 23);
            this.btnAddEvent.TabIndex = 5;
            this.btnAddEvent.Text = "Add";
            this.btnAddEvent.UseVisualStyleBackColor = true;
            this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
            // 
            // btnDeleteEvent
            // 
            this.btnDeleteEvent.Location = new System.Drawing.Point(6, 77);
            this.btnDeleteEvent.Name = "btnDeleteEvent";
            this.btnDeleteEvent.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteEvent.TabIndex = 6;
            this.btnDeleteEvent.Text = "Delete";
            this.btnDeleteEvent.UseVisualStyleBackColor = true;
            this.btnDeleteEvent.Click += new System.EventHandler(this.btnDeleteEvent_Click);
            // 
            // btnEditEvent
            // 
            this.btnEditEvent.Location = new System.Drawing.Point(6, 48);
            this.btnEditEvent.Name = "btnEditEvent";
            this.btnEditEvent.Size = new System.Drawing.Size(75, 23);
            this.btnEditEvent.TabIndex = 7;
            this.btnEditEvent.Text = "Edit";
            this.btnEditEvent.UseVisualStyleBackColor = true;
            this.btnEditEvent.Click += new System.EventHandler(this.btnEditEvent_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.btnSave);
            this.gbSettings.Controls.Add(this.btnPlay);
            this.gbSettings.Controls.Add(this.btnChooseSound);
            this.gbSettings.Controls.Add(this.tbSoundPath);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.numInterval);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Location = new System.Drawing.Point(12, 196);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(253, 135);
            this.gbSettings.TabIndex = 8;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(85, 102);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(10, 102);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(69, 23);
            this.btnPlay.TabIndex = 10;
            this.btnPlay.Text = "Play / Stop";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnChooseSound
            // 
            this.btnChooseSound.Location = new System.Drawing.Point(172, 74);
            this.btnChooseSound.Name = "btnChooseSound";
            this.btnChooseSound.Size = new System.Drawing.Size(69, 23);
            this.btnChooseSound.TabIndex = 9;
            this.btnChooseSound.Text = "Choose...";
            this.btnChooseSound.UseVisualStyleBackColor = true;
            this.btnChooseSound.Click += new System.EventHandler(this.btnChooseSound_Click);
            // 
            // tbSoundPath
            // 
            this.tbSoundPath.Location = new System.Drawing.Point(10, 76);
            this.tbSoundPath.Name = "tbSoundPath";
            this.tbSoundPath.Size = new System.Drawing.Size(156, 20);
            this.tbSoundPath.TabIndex = 3;
            this.tbSoundPath.Text = "(default)";
            this.tbSoundPath.TextChanged += new System.EventHandler(this.tbSoundPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Notification sound (wav)";
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(10, 37);
            this.numInterval.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(94, 20);
            this.numInterval.TabIndex = 1;
            this.numInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numInterval.ValueChanged += new System.EventHandler(this.numInterval_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Repeat notification time (sec)";
            // 
            // chooseWavSoundFile
            // 
            this.chooseWavSoundFile.FileName = "openFileDialog1";
            this.chooseWavSoundFile.Filter = "WAV files(*.wav)|*.wav";
            this.chooseWavSoundFile.Title = "Select notification sound";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status:";
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.btnAddEvent);
            this.gbActions.Controls.Add(this.btnEditEvent);
            this.gbActions.Controls.Add(this.btnDeleteEvent);
            this.gbActions.Location = new System.Drawing.Point(174, 42);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(91, 114);
            this.gbActions.TabIndex = 10;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 343);
            this.Controls.Add(this.gbActions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.lboxAddedKeys);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "Background Key Listener";
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.gbActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ListBox lboxAddedKeys;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Button btnDeleteEvent;
        private System.Windows.Forms.Button btnEditEvent;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnChooseSound;
        private System.Windows.Forms.TextBox tbSoundPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog chooseWavSoundFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbActions;
    }
}

