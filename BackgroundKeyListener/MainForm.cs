using BackgroundKeyListener.Utils;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class MainForm : Form
    {
        private HotkeyHandler hk;
        private AddEventForm addEventForm;
        private EditEventForm editEventForm;
        private ContextMenuStrip eventsMenuStrip;
        private BindingList<Shortcut> eventsList = new BindingList<Shortcut>();

        public MainForm()
        {
            InitializeComponent();
            lboxAddedKeys.Items.Clear();
            lboxAddedKeys.DataSource = eventsList;
            lboxAddedKeys.DisplayMember = "DisplayName";
            addEventForm = new AddEventForm();
            editEventForm = new EditEventForm();
            InitEventsMenuStrip();
            eventsList.ListChanged += EventsList_ListChanged;
            UpdateActionButtons();
        }

        private void EventsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateActionButtons();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            foreach (var shortcut in eventsList)
            {
                shortcut.Listen();
            }

            lbStatus.Text = "Started";
        }

        private void Hk_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            Console.WriteLine("Windows + G pressed!");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (var shortcut in eventsList)
            {
                shortcut.Stop();
            }
            return;

            if (hk?.Registered ?? false)
            {
                hk.Unregister();
                hk.Pressed -= Hk_Pressed; ;
            }
            lbStatus.Text = "Stopped";
        }

        private void Hotkeys()
        {
            hk = new HotkeyHandler();
            hk.KeyCode = Keys.G | Keys.A;
            hk.Pressed += Hk_Pressed; ;

            if (!hk.GetCanRegister(this))
            {
                Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error / visual user feedback");
            }
            else
            {
                hk.Register(this);
            }
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
            } else
            {
                SwitchEnableActionButtons(true);
            }
        }
        private void SwitchEnableActionButtons(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = enable;
        }
    }
}
