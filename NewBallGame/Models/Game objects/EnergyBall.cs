using System;
using System.Drawing;

namespace NewBallGame.Models.Game_objects {
    public class EnergyBall : GameObject {
        public const char DEFAULT_CHAR = '@';
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.Yellow;

        public EnergyBall() : base(DEFAULT_CHAR, new ObjectColor(DEFAULT_COLOR)) { }
    }
}
