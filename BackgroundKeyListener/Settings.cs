using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundKeyListener
{
    public class Settings
    {
        public string Path { get; set; }

        public int RepeatInterval { get; set; }

        public Shortcut[] Shortcuts { get; set; }
    }
}
