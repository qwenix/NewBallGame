using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NewBallGame.Models {

    //   Курсором служит ячейка с определенным цветом фона.
    public class Cursor {
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.DarkGray;

        protected Point _pos;
        protected PlayGround _model;

        //   Текущая позиция курсора.
        public Point Position {
            get => _pos;
            set {
                if (IsInside(value)) {
                    Reset();
                    _pos = value;
                    Set();
                }
            }
        }

        public Cursor(Editor editor) {
            _model = editor;
            _model[Position.X, Position.Y].BackColor = new ObjectColor(DEFAULT_COLOR);
        }

        public Cursor(Game game) {
            _model = game;
            _pos = game.MainBall.Position;

            game[Position.X, Position.Y].BackColor = new ObjectColor(DEFAULT_COLOR);
        }

        public virtual void Set() {
            _model[Position.X, Position.Y].BackColor = new ObjectColor(DEFAULT_COLOR);
            ReDraw();
        }

        protected virtual void Reset() {
            _model[Position.X, Position.Y].BackColor = new ObjectColor(ConsoleColor.Black);
            ReDraw();
        }

        public void MoveUp() {
            Reset();
            Position = new Point(Position.X, Position.Y - 1);
            Set();
        }

        public void MoveDown() {
            Reset();
            Position = new Point(Position.X, Position.Y + 1);
            Set();
        }

        public void MoveLeft() {
            Reset();
            Position = new Point(Position.X - 1, Position.Y);
            Set();
        }

        public void MoveRight() {
            Reset();
            Position = new Point(Position.X + 1, Position.Y);
            Set();
        }

        private void ReDraw() {
            _model.View.Draw(Position);
        }

        private bool IsInside(Point p) {
            return p.X >= 0 && p.X < PlayGround.W && p.Y >= 0 && p.Y < PlayGround.H;
        }

    }
}
