using BackgroundKeyListener.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class EditEventForm : Form
    {

        public string Shortcut = string.Empty;

        private LowLevelKeyboardHanler keyboardHanler = new LowLevelKeyboardHanler();
        private List<Keys> pressed = new List<Keys>();
        private bool unpressed;

        public EditEventForm()
        {
            InitializeComponent();
            FormClosing += AddEventForm_FormClosing;
            Shown += AddEventForm_Shown;
        }

        private void AddEventForm_Shown(object sender, EventArgs e)
        {
            StartListen();
        }

        private void AddEventForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Shortcut = tbKey.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditEventForm_Shown(object sender, EventArgs e)
        {
            tbKey.Text = Shortcut;
        }

        private void StartListen()
        {
            keyboardHanler.OnKeyPressed += KeyboardHanler_OnKeyPressed;
            keyboardHanler.OnKeyUnpressed += KeyboardHanler_OnKeyUnpressed;
            keyboardHanler.HookKeyboard();
        }

        private void KeyboardHanler_OnKeyUnpressed(object sender, Keys e)
        {
            unpressed = true;
        }

        private void KeyboardHanler_OnKeyPressed(object sender, Keys e)
        {
            if (unpressed)
            {
                pressed.Clear();
                unpressed = false;
            }

            if (!pressed.Contains(e))
            {
                pressed.Add(e);
            }
            tbKey.Text = string.Join("+", pressed);
        }

        private void StopListen()
        {
            keyboardHanler.UnHookKeyboard();
        }
    }
}
