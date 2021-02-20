using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {

    public class Teleport : GameObject {
        public const char DEFAULT_CHAR = '©';
        private const int MAX_COLORS = 7;

        private static int colorId = MAX_COLORS + 1;


        public int CurrentColorId { get => (int)ForeColor.ConsoleColor; }

        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }

        private int ColorId {
            get => colorId;
            set {
                if (value < 2)
                    colorId = 7;
                else if (value > 7)
                    colorId = 2;
                else
                    colorId = value;
            }
        }

        public Teleport(Point p) {
            Char = DEFAULT_CHAR;
            ForeColor = new ObjectColor((ConsoleColor)ColorId--);
            FirstPoint = new Point(p.X, p.Y);
            SecondPoint = new Point(-1, -1);
        }

        public static void ResetColor() {
            colorId = MAX_COLORS + 1;
        }

        public static void RefreshColor() {
            colorId = colorId - 1 > 2 ? colorId - 1 : MAX_COLORS;
        }

        public void AddExitPoint(Point p) {
            if (FirstPoint.X == -1)
                FirstPoint = p;
            else
                SecondPoint = p;
        }

        public void RemoveExitPoint(Point p) {
            if (FirstPoint.Equals(p))
                FirstPoint = new Point(-1, -1);
            else
                SecondPoint = new Point(-1, -1);
        }

        public bool IsTeleport() {
            return FirstPoint.X != -1 && SecondPoint.X != -1;
        }

        public bool HasAnyPoint() {
            return FirstPoint.X != -1 || SecondPoint.X != -1;
        }

        public bool HasPoint(Point p) {
            return FirstPoint.Equals(p) || SecondPoint.Equals(p);
        }

        public Point GetOutPoint(Point inPoint) {
            if (Equals(inPoint, FirstPoint))
                return SecondPoint;
            else
                return FirstPoint;
        }
    }
}
