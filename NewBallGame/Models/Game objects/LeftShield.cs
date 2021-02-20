using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {
    public class LeftShield : Shield {
        public const char DEFAULT_CHAR = '\\';

        public LeftShield() : base(DEFAULT_CHAR) { }
    }
}
