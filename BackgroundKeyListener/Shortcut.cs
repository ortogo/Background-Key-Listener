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
        public DateTime LastPressed;
        public bool IsListen;

        public bool IsExpired
        {
            get
            {
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long lpm = LastPressed.Ticks / TimeSpan.TicksPerMillisecond;
                return ((milliseconds - lpm) / 1000) > Timeout;
            }
        }

        private Dictionary<Keys, bool> states;

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
            LastPressed = DateTime.Now;

            if (IsListen)
            {
                return;
            }

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
                states = new Dictionary<Keys, bool>();
                foreach(var key in Keys)
                {
                    states.Add(key, false);
                }
                keyboardHanler.OnKeyPressed += KeyboardHanler_OnKeyPressed1;
                keyboardHanler.OnKeyUnpressed += KeyboardHanler_OnKeyUnpressed1;
                keyboardHanler.HookKeyboard();
            }

            IsListen = true;
        }

        private void KeyboardHanler_OnKeyUnpressed1(object sender, Keys e)
        {
            CheckCombo();

            if (states.ContainsKey(e))
            {
                states[e] = false;
            }
        }

        private void KeyboardHanler_OnKeyPressed1(object sender, Keys e)
        {
            if (states.ContainsKey(e))
            {
                states[e] = true;
            }
        }

        private void KeyboardHanler_OnKeyUnpressed(object sender, Keys e)
        {
            if (e == Keys[0])
            {
                LastPressed = DateTime.Now;
                Console.WriteLine("Unpressed {0}", Keys[0]);
            }
        }

        private void KeyboardHanler_OnKeyPressed(object sender, Keys e)
        {
            
        }

        private void CheckCombo()
        {
            var combo = true;
            foreach (var state in states.Values)
            {
                if (state == false)
                {
                    combo = false;
                    break;
                }
            }

            if (combo)
            {
                LastPressed = DateTime.Now;
                Console.WriteLine("Combo {0}", ToString());
            }
        }

        public void Stop()
        {
            if(!IsListen)
            {
                return;
            }

            if (Keys.Count == 1)
            {
                keyboardHanler.OnKeyPressed -= KeyboardHanler_OnKeyPressed;
                keyboardHanler.OnKeyUnpressed -= KeyboardHanler_OnKeyUnpressed;
                keyboardHanler.UnHookKeyboard();
            }

            if (Keys.Count > 1)
            {
                states?.Clear();
                states = null;
                keyboardHanler.OnKeyPressed -= KeyboardHanler_OnKeyPressed1;
                keyboardHanler.OnKeyUnpressed -= KeyboardHanler_OnKeyUnpressed1;
                keyboardHanler.HookKeyboard();
            }

            IsListen = false;
        }

        public override string ToString()
        {
            return $"{string.Join("+", Keys)} (timeout {Timeout})";
        }
    }
}
