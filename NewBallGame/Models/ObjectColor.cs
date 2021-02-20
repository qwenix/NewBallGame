using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models {
    public struct ObjectColor {
        private static Dictionary<Color, ConsoleColor> _map;

        public ConsoleColor ConsoleColor { get; }
        public Color FormColor { get; }

        public ObjectColor(ConsoleColor color) {
            ConsoleColor = color;
            FormColor = Color.Black;

            foreach (var p in _map) {
                if (p.Value.Equals(color)) {
                    FormColor = p.Key;
                    break;
                }
            }
        }

        public ObjectColor(Color color) {
            FormColor = color;
            ConsoleColor = _map[color];
        }

        static ObjectColor() {
            _map = new Dictionary<Color, ConsoleColor> {
                [Color.Green] = ConsoleColor.Green,
                [Color.Black] = ConsoleColor.Black,
                [Color.Yellow] = ConsoleColor.Yellow,
                [Color.DarkGreen] = ConsoleColor.DarkGreen,
                [Color.DarkMagenta] = ConsoleColor.DarkMagenta,
                [Color.Gray] = ConsoleColor.Gray,
                [Color.DarkGray] = ConsoleColor.DarkGray,
                [Color.Blue] = ConsoleColor.Blue,
                [Color.Cyan] = ConsoleColor.Cyan,
                [Color.Red] = ConsoleColor.Red,
                [Color.DarkBlue] = ConsoleColor.DarkBlue,
                [Color.DarkCyan] = ConsoleColor.DarkCyan,
                [Color.DarkRed] = ConsoleColor.DarkRed,
                [Color.YellowGreen] = ConsoleColor.DarkYellow,
                [Color.Magenta] = ConsoleColor.Magenta,
                [Color.White] = ConsoleColor.White
            };
        }
    }
}
