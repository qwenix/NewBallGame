using NewBallGame.Data;
using System;
using System.Drawing;

namespace NewBallGame.Models.Game_objects {
    public class Wall : GameObject {
        public const char DEFAULT_CHAR = '#';
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.Magenta;

        public Wall() : base(DEFAULT_CHAR, new ObjectColor(DEFAULT_COLOR)) { }
    }
}
