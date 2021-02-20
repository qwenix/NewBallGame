using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {
    public class Trap : GameObject {
        public const char DEFAULT_CHAR = 'x';
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.Cyan;

        public Trap() : base(DEFAULT_CHAR, new ObjectColor(DEFAULT_COLOR)) { }
    }
}
