using System;
using System.Drawing;

namespace NewBallGame.Models.Game_objects {
    public class Ball : GameObject {
        public const char DEFAULT_CHAR = 'o';
        public const ConsoleColor DEFAULT_COLOR = ConsoleColor.Green;

        // Скорость и позиция шара относительно матрицы игрового поля.
        public int Vx { get; set; }
        public int Vy { get; set; }

        public Point Position { get; set; }

        public bool IsFrozen { get; set; }
        public int FrozenTime { get; set; }

        protected virtual Game Game { get; set; }

        public Ball(Game game) : base(DEFAULT_CHAR, new ObjectColor(DEFAULT_COLOR)) {
            Game = game;
            AddStartSpeed();
        }

        public virtual void Move() {
            if (IsFrozen) {
                if (FrozenTime == 0) {
                    IsFrozen = false;
                    Game[Position.X, Position.Y].BackColor = new ObjectColor(ConsoleColor.Black);
                }
                else
                    FrozenTime--;

                return;
            }

            Point _p = new Point(Position.X + Vx, Position.Y + Vy);

            Step(ref _p);
            TrySwapCells(_p);
        }

        protected virtual void AddStartSpeed() {
            switch (DateTime.Now.Millisecond % 2) {
                case 0:
                    Vy++;
                    break;
                case 1:
                    Vx++;
                    break;
            }
        }

        private bool CanStep(Point p) {
            GameObject gameObj = Game[p.X, p.Y];

            return gameObj is Blank || gameObj is EnergyBall;
        }

        protected void StepAhead(ref Point p) {
            StepAhead(ref p, Vx, Vy);
        }

        protected void StepAhead(ref Point p, int vx, int vy) {
            p = new Point(p.X + vx, p.Y + vy);
        }

        protected void KnockOf(Shield s) {
            int _vx = Vx;
            int _vy = Vy;

            KnockOf(s, ref _vx, ref _vy);
            Vx = _vx;
            Vy = _vy;
        }

        protected void KnockOf(Shield s, ref int vx, ref int vy) {
            int t = vx;
            if (s is LeftShield) {
                vx = vx == 0 ? vy : 0;
                vy = vx == 0 ? t : 0;
            }
            else {
                vx = vx == 0 ? -vy : 0;
                vy = vx == 0 ? -t : 0;
            }
        }

        private void Step(ref Point p) {
            while (!CanStep(p)) {
                GameObject gameObj = Game[p.X, p.Y];
                if (gameObj is Wall) {
                    Vy = -Vy;
                    Vx = -Vx;
                }
                else if (gameObj is Shield s) {
                    KnockOf(s);
                    StepAhead(ref p);
                    continue;
                }
                else if (gameObj is Trap) {
                    IsFrozen = true;
                    FrozenTime = 30;
                    Game[p.X, p.Y].BackColor = new ObjectColor(ConsoleColor.White);
                    break;
                }
                else if (gameObj is Teleport) {
                    Teleport tel = Game[p.X, p.Y] as Teleport;
                    p = tel.GetOutPoint(p);
                }
                StepAhead(ref p);
            }
        }

        private bool TrySwapCells(Point p) {
            if (p.Equals(Position)) return false;
            if (Game[p.X, p.Y] is EnergyBall)
                Game.RefreshScore();

            ObjectColor tc = BackColor;
            BackColor = Game[p.X, p.Y].BackColor;
            Game[p.X, p.Y] = this;
            Game[Position.X, Position.Y] = new Blank() { BackColor = tc };

            Game.View.Draw(p.X, p.Y);
            Game.View.Draw(Position.X, Position.Y);

            Position = p;
            return true;
        }
    }
}
