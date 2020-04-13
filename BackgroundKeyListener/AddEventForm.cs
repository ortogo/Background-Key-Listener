using BackgroundKeyListener.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public partial class AddEventForm : Form
    {
        private const string PRESS_ANY_KEY = "Press any key...";

        public Shortcut ShortcutCustom { get; set; }

        private LowLevelKeyboardHanler keyboardHanler = new LowLevelKeyboardHanler();
        private List<Keys> pressed = new List<Keys>();
        private bool unpressed;

        public AddEventForm()
        {
            InitializeComponent();
            FormClosing += AddEventForm_FormClosing;
            Shown += AddEventForm_Shown;
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

        private void AddEventForm_Shown(object sender, EventArgs e)
        {
            tbKey.Text = PRESS_ANY_KEY;
            tbKey.Focus();
            numTimeout.Value = 5;
        }

        private void AddEventForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopListen();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!tbKey.Text.Equals(PRESS_ANY_KEY))
            {
                ShortcutCustom = new Shortcut
                {
                    Keys = pressed,
                    Timeout = (int)numTimeout.Value
                };
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
