using BackgroundKeyListener.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BackgroundKeyListener
{
    public class Shortcut
    {
        public List<Keys> Keys;
        public int Timeout;

        public string DisplayName
        {
            get
            {
                return $"{string.Join("+", Keys)} (timeout {Timeout})";
            }
        }

        private LowLevelKeyboardHanler keyboardHanler = new LowLevelKeyboardHanler();

        public void Listen()
        {
            if (Keys.Count < 1)
            {
                throw new Exception("Key or shortcut is empty for event!");
            }

            if (Keys.Count == 1)
            {
                keyboardHanler.OnKeyPressed += KeyboardHanler_OnKeyPressed;
                keyboardHanler.OnKeyUnpressed += KeyboardHanler_OnKeyUnpressed;
                keyboardHanler.HookKeyboard();
            }

            if (Keys.Count > 1)
            {
                // listen for shortcut
            }
        }

        private void KeyboardHanler_OnKeyUnpressed(object sender, Keys e)
        {
            if (e == Keys[0])
            {
                Console.WriteLine("Unpressed {0}", Keys[0]);
            }
        }

        private void KeyboardHanler_OnKeyPressed(object sender, Keys e)
        {
            
        }

        public void Stop()
        {
            if (Keys.Count == 1)
            {
                keyboardHanler.OnKeyPressed -= KeyboardHanler_OnKeyPressed;
                keyboardHanler.OnKeyUnpressed -= KeyboardHanler_OnKeyUnpressed;
                keyboardHanler.UnHookKeyboard();
            }

            if (Keys.Count > 1)
            {
                // listen for shortcut
            }
        }

        public override string ToString()
        {
            return $"{string.Join("+", Keys)} (timeout {Timeout})";
        }
    }
}
