using NewBallGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NewBallGame.Models.Game_objects;
using System.Windows.Forms;

namespace NewBallGame.Models {
    public class Game : PlayGround {
        //    Интервал времени (мс) за который шар делает шаг.
        public const int TIMER_INTERVAL = 50;

        public int StartScore { get; set; }

        public bool IsLvlCompleted { get => EnergyCounter == 0; }
        public bool IsLvlFailed { get => Timer == 0; }
        public bool IsGameOver { get; set; }

        public Ball MainBall { get; set; }

        public override int Timer {
            get => base.Timer;
            set {
                base.Timer = value;
                if (value == 0) {
                    Paused = true;
                    View.ShowMessage("TIME IS OVER!");
                }
            }
        }

        public Game(IPlayableController mainController) : this(1, 0, 5, mainController) { }

        public Game(int lvl, int score, int lives, IPlayableController mainController) {
            CurrentLevel = DataReader.GetLevelInfo(lvl);
            EnergyCounter = CurrentLevel.EnergyBallNumber;
            Timer = CurrentLevel.Time;

            Score = score;
            StartScore = score;
            Lives = lives;

            GenerateCells();
            AddBall();

            Cursor = new Cursor(this);
            MainController = mainController;
        }

        public override void Step() {
            MainBall.Move();
        }

        public override void Resume() {
            if (EnergyCounter == 0)
                MainController.Start(new Game(CurrentLevel.Id + 1, Score, Lives, MainController));
            else if (Timer != 0)
                base.Resume();
            else
                Restart();
        }

        public void RefreshScore() {
            EnergyCounter--;
            Score += 15;
            View.DrawStatePanel();
            if (EnergyCounter == 0) {
                Paused = true;
                View.ShowMessage("YOU WIN!");
            }
        }

        public override void ExecuteAction(ConsoleKey key) {
            switch (key) {
                case ConsoleKey.Z:
                    AddShield(new LeftShield());
                    break;
                case ConsoleKey.X:
                    AddShield(new RightShield());
                    break;
                case ConsoleKey.Spacebar:
                    RemoveShield();
                    break;
                case ConsoleKey.Escape:
                    Paused = true;
                    MainController.ShowMenu(MenuMode.Pause);
                    break;
                default:
                    base.ExecuteAction(key);
                    break;
            }
        }

        public override void ExecuteAction(Keys key) {
            switch (key) {
                case Keys.Z:
                    AddShield(new LeftShield());
                    break;
                case Keys.X:
                    AddShield(new RightShield());
                    break;
                case Keys.Space:
                    RemoveShield();
                    break;
                case Keys.Escape:
                    Paused = true;
                    MainController.ShowMenu(MenuMode.Pause);
                    break;
                default:
                    base.ExecuteAction(key);
                    break;
            }
        }

        public void Restart() {
            if (Lives > 1)
                MainController.Start(new Game(CurrentLevel.Id, StartScore, Lives - 1, MainController));
            else
                MainController.Start(new Game(MainController));
        }

        public void RemoveShield() {
            RemoveShield(Cursor.Position);
        }

        public void RemoveShield(Point p) {
            if (this[p.X, p.Y] is Shield) {
                RemoveHiddenShield(p);
                this[p.X, p.Y].BackColor = new ObjectColor(Cursor.DEFAULT_COLOR);
                Score = (int)(Score * 0.95);

                View.Draw(Cursor.Position);
                View.DrawStatePanel();
            }
        }

        public void RemoveHiddenShield(Point p) {
            this[p.X, p.Y] = new Blank();
        }

        public void AddShield(Shield shield) {
            if (this[Cursor.Position.X, Cursor.Position.Y] is Blank) {
                AddHiddenShield(shield, Cursor.Position);
                Score = (int)(Score * 0.97);

                View.DrawStatePanel();
                View.Draw(Cursor.Position);
            }
        }

        public void AddHiddenShield(Shield shield, Point p) {
            shield.BackColor = new ObjectColor(Cursor.DEFAULT_COLOR);
            this[p.X, p.Y] = shield;
        }

        private void InitializeProperties(int lvl, int score, int lives) {
            CurrentLevel = DataReader.GetLevelInfo(lvl);
            EnergyCounter = CurrentLevel.EnergyBallNumber;
            Timer = CurrentLevel.Time;

            Score = score;
            StartScore = score;
            Lives = lives;

            GenerateCells();
            AddBall();
        }

        protected virtual void AddBall() {
            MainBall = new Ball(this) { Position = new Point(1, 1) };
            this[1, 1] = MainBall;
        }
    }
}
