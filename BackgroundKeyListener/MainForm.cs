using BackgroundKeyListener.Utils;
using System;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class MainForm : Form
    {
        private HotkeyHandler hk;
        private LowLevelKeyboardHanler lvkh;
        private AddEventForm addEventForm;
        private EditEventForm editEventForm;
        private ContextMenuStrip eventsMenuStrip;

        public MainForm()
        {
            InitializeComponent();
            //lboxAddedKeys.Items.Clear();
            addEventForm = new AddEventForm();
            editEventForm = new EditEventForm();
            InitEventsMenuStrip();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            return;
            lvkh = new LowLevelKeyboardHanler();
            lvkh.OnKeyPressed += Lvkh_OnKeyPressed;
            lvkh.OnKeyUnpressed += Lvkh_OnKeyUnpressed;
            lvkh.HookKeyboard();
            lbStatus.Text = "Started";
        }

        private void Lvkh_OnKeyUnpressed(object sender, Keys e)
        {
            Console.WriteLine($"Key {e.ToString()}");
        }

        private void Lvkh_OnKeyPressed(object sender, Keys e)
        {
            Console.WriteLine($"Key {e.ToString()}");
        }

        private void Hk_Pressed(object sender, System.ComponentModel.HandledEventArgs e)
        {
            Console.WriteLine("Windows + G pressed!");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            return;

            if (hk?.Registered ?? false)
            {
                hk.Unregister();
                hk.Pressed -= Hk_Pressed; ;
            }

            lvkh?.UnHookKeyboard();
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
                var selectedMenuItem = lboxAddedKeys.Items[index].ToString();
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
                lboxAddedKeys.Items.Add(addEventForm.Shortcut);
            }
        }

        private void EditEvent()
        {
            var selectedEventIndex = lboxAddedKeys.SelectedIndex;

            if (selectedEventIndex < 0)
            {
                return;
            }

            var selectedEventItem = lboxAddedKeys.SelectedItem;

            editEventForm.Shortcut = selectedEventItem.ToString();

            var result = editEventForm.ShowDialog();
            switch (result)
            {
                case DialogResult.OK:
                    lboxAddedKeys.Items.RemoveAt(selectedEventIndex);
                    lboxAddedKeys.Items.Insert(selectedEventIndex, editEventForm.Shortcut);
                    break;
                case DialogResult.Yes:
                    lboxAddedKeys.Items.RemoveAt(selectedEventIndex);
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
                lboxAddedKeys.Items.RemoveAt(selectedEventIndex);
            }
        }
    }
}
