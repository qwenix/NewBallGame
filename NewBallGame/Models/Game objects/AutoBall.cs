using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models.Game_objects {
    class AutoBall : Ball {
        protected override Game Game {
            get => base.Game as AIGame;
            set => base.Game = value;
        }

        public AutoBall(Game game) : base(game) { }

        public override void Move() {
            if (!IsCurrentWayCorrect()) {
                int vx = Vx;
                int vy = Vy;

                if (IsNextHasWall()) {
                    vx = -vx;
                    vy = -vy;
                }

                for (int i = 1; i < 4; i++) {
                    if (ChoseOtherWay(Position, i, vx, vy))
                        break;
                }
            }
            base.Move();
        }

        private bool ChoseOtherWay(Point current, int shields, int vx, int vy) {
            if (shields == 0)
                return false;

            Point start = current;
            StepAhead(ref current, vx, vy);

            while (!(Game[current.X, current.Y] is Wall)) {
                if (Game[current.X, current.Y] is Blank) {
                    Shield s = new LeftShield();
                    Game.AddHiddenShield(s, current);

                    int _vx = vx;
                    int _vy = vy;
                    KnockOf(s, ref _vx, ref _vy);

                    if (HasEnergy(start, vx, vy) || ChoseOtherWay(current, shields - 1, _vx, _vy)) {
                        Game.Cursor.Position = current;
                        return true;
                    }
                    else {
                        Game.RemoveHiddenShield(current);
                        KnockOf(s, ref _vx, ref _vy);

                        s = new RightShield();
                        KnockOf(s, ref _vx, ref _vy);
                        Game.AddHiddenShield(s, current);
                        if (HasEnergy(start, vx, vy) || ChoseOtherWay(current, shields - 1, _vx, _vy)) {
                            Game.Cursor.Position = current;
                            return true;
                        }

                        Game.RemoveHiddenShield(current);
                    }
                }
                else if (Game[current.X, current.Y] is Shield s)
                    KnockOf(s, ref vx, ref vy);
                else if (Game[current.X, current.Y] is Teleport t)
                    current = t.GetOutPoint(current);
                StepAhead(ref current, vx, vy);
            }
            return false;
        }


        private bool IsCurrentWayCorrect() {
            return HasEnergy(Position, Vx, Vy) || HasEnergy(Position, -Vx, -Vy) ||
                IsNoWay(Position, Vx, Vy);
        }

        private bool HasEnergy(Point p, int vx, int vy) {
            StepAhead(ref p, vx, vy);
            Point copy = p;
            bool cycle = false;
            int number = 0;

            while (!(Game[p.X, p.Y] is Wall) && !(Game[p.X, p.Y] is EnergyBall)) {
                if (Game[p.X, p.Y] is Teleport t)
                    p = t.GetOutPoint(p);
                else if (Game[p.X, p.Y] is Shield s) {
                    KnockOf(s, ref vx, ref vy);
                    number++;
                }
                else if (p == copy) {
                    if (cycle)
                        return false;
                    else cycle = true;
                }
                else if (number == 10)
                    return false;
                StepAhead(ref p, vx, vy);
            }
            return Game[p.X, p.Y] is EnergyBall;
        }

        private bool IsNextHasWall() {
            int x = Position.X + Vx;
            int y = Position.Y + Vy;

            return Game[x, y] is Wall;
        }

        private bool IsNoWay(Point p, int vx, int vy) {
            StepAhead(ref p, vx, vy);
            Point p1, p2;

            if (vx == 0) {
                p1 = new Point(p.X + Vy, p.Y);
                p2 = new Point(p.X - Vy, p.Y);
            }
            else {
                p1 = new Point(p.X, p.Y + vx);
                p2 = new Point(p.X, p.Y - vx);
            }

            while (!Game.IsOnFrame(p1) || !Game.IsOnFrame(p2)) {
                if (!(Game[p1.X, p1.Y] is Wall) || !(Game[p2.X, p2.Y] is Wall))
                    return false;
                StepAhead(ref p1, vx, vy);
                StepAhead(ref p2, vx, vy);
            }
            return true;
        }

        protected override void AddStartSpeed() {
            Vx = 1;
            Vy = 0;
        }
    }
}
