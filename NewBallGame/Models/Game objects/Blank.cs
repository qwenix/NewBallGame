using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {
    public class Blank : GameObject {
        public const char DEFAULT_CHAR = '\0';
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.Black;

        public Blank() : base(DEFAULT_CHAR, new ObjectColor(DEFAULT_COLOR)) { }
    }
}
