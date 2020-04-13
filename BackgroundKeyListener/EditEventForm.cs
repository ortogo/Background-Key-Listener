using BackgroundKeyListener.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class EditEventForm : Form
    {

        public Shortcut Shortcut;

        private LowLevelKeyboardHanler keyboardHanler = new LowLevelKeyboardHanler();
        private List<Keys> pressed = new List<Keys>();
        private bool unpressed;

        public EditEventForm()
        {
            InitializeComponent();
            FormClosing += EditEventForm_FormClosing;
            Shown += EditEventForm_Shown;
            tbKey.GotFocus += TbKey_GotFocus;
            tbKey.LostFocus += TbKey_LostFocus;
        }

        private void TbKey_LostFocus(object sender, EventArgs e)
        {
            StopListen();
        }

        private void TbKey_GotFocus(object sender, EventArgs e)
        {
            StartListen();
        }

        private void EditEventForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Shortcut = new Shortcut
            {
                Keys = pressed,
                Timeout = (int)numTimeout.Value
            };
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
            tbKey.Text = string.Join("+", Shortcut.Keys);
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
