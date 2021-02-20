using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {
    public class RightShield : Shield {
        public const char DEFAULT_CHAR = '/';

        public RightShield() : base(DEFAULT_CHAR) { }
    }
}
