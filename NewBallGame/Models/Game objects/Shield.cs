using System;

namespace NewBallGame.Models.Game_objects {
    public abstract class Shield : GameObject {
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

        public Shield(char c) : base(c, new ObjectColor(DEFAULT_COLOR)) { }

    }
}
