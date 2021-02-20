using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame {
    class Cursor {
        private const ConsoleColor DEFAULT_COLOR = ConsoleColor.White;

        public int Row { get; set; }
        public int Col { get; set; }
        public Game Game { get; set; }

        public Cursor(Game game) {
            Game = game;
            Row = game.MainBall.Row;
            Col = game.MainBall.Col;

            Set();
        }

        public void Move() {
            switch(Console.ReadKey(true).Key) {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }

        private void Reset() {
            Game.Cells[Row, Col].BgColor = ConsoleColor.Black;
            View.Draw(Game.Cells, Row, Col);
        }
        private void Set() {
            Game.Cells[Row, Col].BgColor = DEFAULT_COLOR;
            View.Draw(Game.Cells, Row, Col);
        }

        private void MoveUp() {
            if (Row - 1 >= 0) {
                Reset();
                Row--;
                Set();
            }
        }

        private void MoveDown() {
            if (Row + 1 < Game.H) {
                Reset();
                Row++;
                Set();
            }
        }

        private void MoveLeft() {
            if (Col - 1 >= 0) {
                Reset();
                Col--;
                Set();
            }
        }
        private void MoveRight() {
            if (Col + 1 < Game.W) {
                Reset();
                Col++;
                Set();
            }
        }

    }
}
