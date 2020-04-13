using BackgroundKeyListener.Properties;
using BackgroundKeyListener.Utils;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class MainForm : Form
    {
        private AddEventForm addEventForm;
        private EditEventForm editEventForm;
        private ContextMenuStrip eventsMenuStrip;
        private BindingList<Shortcut> eventsList = new BindingList<Shortcut>();
        private BackgroundWorker TimeoutWatcher;
        private SoundPlayer soundPlayer;
        private Stream soundSource;
        private bool isSoundPlayed;
        private bool isSettingsLoading = true;

        private Settings settings;

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            lboxAddedKeys.Items.Clear();
            lboxAddedKeys.DataSource = eventsList;
            lboxAddedKeys.DisplayMember = "DisplayName";
            addEventForm = new AddEventForm();
            editEventForm = new EditEventForm();
            InitEventsMenuStrip();
            eventsList.ListChanged += EventsList_ListChanged;
            UpdateActionButtons();
            InitTimeouWatcher();
            InitSoundPlayer();
        }

        private void InitSoundPlayer()
        {
            if (!File.Exists(settings.Path))
            {
                soundSource = Resources.NotifiactionSound;
            }
            else
            {
                soundSource = File.OpenRead(settings.Path);
            }
            soundPlayer = new SoundPlayer(soundSource);
            soundPlayer.Load();
        }

        private void InitTimeouWatcher()
        {
            TimeoutWatcher = new BackgroundWorker();
            TimeoutWatcher.WorkerSupportsCancellation = true;
            TimeoutWatcher.WorkerReportsProgress = true;
            TimeoutWatcher.DoWork += TimeoutWatcher_DoWork;
            TimeoutWatcher.RunWorkerCompleted += TimeoutWatcher_RunWorkerCompleted;
            TimeoutWatcher.ProgressChanged += TimeoutWatcher_ProgressChanged;
        }

        private void TimeoutWatcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void TimeoutWatcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void TimeoutWatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                foreach (var ev in eventsList)
                {
                    if (ev.IsExpired && ev.IsListen)
                    {
                        Console.WriteLine("Expired {0}, last pressed {1}.", ev.ToString(), ev.LastPressed);

                        PlayNotificationSound();
                        ev.LastPressed = ev.LastPressed.AddSeconds((double)numInterval.Value);
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void PlayNotificationSound()
        {
            if (isSoundPlayed)
            {
                return;
            }

            if (soundPlayer == null)
            {
                InitSoundPlayer();
            }

            var t = Task.Factory.StartNew(() =>
            {
                PlayWithEndNotify(OnPlayEnd);
            });

            isSoundPlayed = true;
        }

        private void PlayWithEndNotify(Action endNotify)
        {
            soundPlayer.PlaySync();
            endNotify?.Invoke();
        }

        private void OnPlayEnd()
        {
            BeginInvoke((ThreadStart)delegate ()
            {
                isSoundPlayed = false;
            });
        }

        private void EventsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateActionButtons();

            if(isSettingsLoading)
            {
                return;
            }

            SaveSettings();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            EnableDisableControls(false);
            foreach (var shortcut in eventsList)
            {
                shortcut.Listen();
            }

            TimeoutWatcher.RunWorkerAsync();

            lbStatus.Text = "Started";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            EnableDisableControls(true);
            TimeoutWatcher.CancelAsync();

            foreach (var shortcut in eventsList)
            {
                shortcut.Stop();
            }

            lbStatus.Text = "Stopped";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            AddEvent();
        }

        private void btnEditEvent_Click(object sender, EventArgs e)
        {
            EditEvent();
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            DeleteEvent();
        }

        private void InitEventsMenuStrip()
        {
            var editMenuItem = new ToolStripMenuItem
            {
                Text = "Edit"
            };
            var deleteMenuItem = new ToolStripMenuItem
            {
                Text = "Delete"
            };
            editMenuItem.Click += EditMenuItem_Click;
            deleteMenuItem.Click += DeleteMenuItem_Click;

            eventsMenuStrip = new ContextMenuStrip();
            eventsMenuStrip.Items.Add(editMenuItem);
            eventsMenuStrip.Items.Add(deleteMenuItem);
            lboxAddedKeys.ContextMenuStrip = eventsMenuStrip;
            lboxAddedKeys.MouseDown += LboxAddedKeys_MouseDown;
        }

        private void LboxAddedKeys_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = lboxAddedKeys.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var selectedMenuItem = eventsList[index].ToString();
                eventsMenuStrip.Show(Cursor.Position);
                eventsMenuStrip.Visible = true;
            }
            else
            {
                eventsMenuStrip.Visible = false;
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteEvent();
        }

        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditEvent();
        }

        private void AddEvent()
        {
            var result = addEventForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                eventsList.Add(addEventForm.ShortcutCustom);
            }
        }

        private void EditEvent()
        {
            var selectedEventIndex = lboxAddedKeys.SelectedIndex;

            if (selectedEventIndex < 0)
            {
                return;
            }

            editEventForm.Shortcut = eventsList[selectedEventIndex];

            var result = editEventForm.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    eventsList.RemoveAt(selectedEventIndex);
                    eventsList.Insert(selectedEventIndex, editEventForm.Shortcut);
                    break;
                case DialogResult.Yes:
                    eventsList.RemoveAt(selectedEventIndex);
                    break;
            }
        }

        private void DeleteEvent()
        {
            var selectedEventIndex = lboxAddedKeys.SelectedIndex;

            if (selectedEventIndex < 0)
            {
                return;
            }

            var selectedEventItem = lboxAddedKeys.SelectedItem;
            var confirmDelete = MessageBox.Show(
                $"Delete {selectedEventItem}?",
                "Delete event",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            if (confirmDelete == DialogResult.Yes)
            {
                eventsList.RemoveAt(selectedEventIndex);
            }
        }

        private void UpdateActionButtons()
        {
            if (eventsList.Count < 1)
            {
                SwitchEnableActionButtons(false);
            }
            else
            {
                SwitchEnableActionButtons(true);
            }
        }
        private void SwitchEnableActionButtons(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = enable;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if(isSoundPlayed)
            {
                soundPlayer.Stop();
            }
            else
            {
                PlayNotificationSound();
            }
        }

        private void btnChooseSound_Click(object sender, EventArgs e)
        {
            var result = chooseWavSoundFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }

            string filename = chooseWavSoundFile.FileName;
            tbSoundPath.Text = filename;
            settings.Path = filename;
            soundSource = File.OpenRead(filename);
            InitSoundPlayer();
        }

        private void SaveSettings()
        {
            if (settings == null)
            {
                settings = new Settings();
            }
            settings.Path = tbSoundPath.Text;
            settings.RepeatInterval = (int)numInterval.Value;
            settings.Shortcuts = new Shortcut[eventsList.Count];
            eventsList.CopyTo(settings.Shortcuts, 0);

            string serialized = JsonConvert.SerializeObject(settings);

            if (!File.Exists(SettingsPath))
            {
                Directory.CreateDirectory(SettingsDir);
                File.Create(SettingsPath).Close();
            }

            File.WriteAllText(SettingsPath, serialized);
        }

        private void LoadSettings()
        {
            isSettingsLoading = true;

            if (!File.Exists(SettingsPath))
            {
                SaveSettings();
            }
            else
            {
                string serialized = File.ReadAllText(SettingsPath);
                // if settings file empty
                if (string.IsNullOrEmpty(serialized))
                {
                    SaveSettings();
                    return;
                }
                settings = JsonConvert.DeserializeObject<Settings>(serialized);
            }

            tbSoundPath.Text = settings.Path;
            numInterval.Value = settings.RepeatInterval;

            foreach(var sh in settings.Shortcuts)
            {
                eventsList.Add(sh);
            }

            isSettingsLoading = false;
        }

        private string SettingsPath = $"{PathLocal}\\{LOCALAPPDIR}\\{FILENAME}";
        private string SettingsDir = $"{PathLocal}\\{LOCALAPPDIR}";
        private const string FILENAME = "settings.json";
        private const string LOCALAPPDIR = "BackgroundKeyListener";
        static readonly string PathLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private void numInterval_ValueChanged(object sender, EventArgs e)
        {
            settings.RepeatInterval = (int)numInterval.Value;
        }

        private void tbSoundPath_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void EnableDisableControls(bool enable)
        {
            lboxAddedKeys.Enabled = enable;
            gbActions.Enabled = enable;
            gbSettings.Enabled = enable;
        }
    }
}
