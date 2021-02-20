using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NewBallGame.Data;
using System.Drawing;
using NewBallGame.Models.Game_objects;
using System.Windows.Forms;
using NewBallGame.Controllers;

namespace NewBallGame.Models {
    public struct LevelInfo {
        public int Id;
        public int EnergyBallNumber;
        public int Time;
        public char[,] Chars;
        public List<Teleport> Teleports;
    }

    public abstract class PlayGround {
        public const int W = 68;
        public const int H = 34;

        public IPlayableController MainController;

        protected GameObject[,] GameObjects;

        private int _timer;
        private bool _isPaused;

        public GameObject this[int x, int y] {
            get {
                if (IsLocationCorrect(x, y))
                    return GameObjects[x, y];
                else
                    return null;
            }
            set {
                if (IsLocationCorrect(x, y))
                    GameObjects[x, y] = value;
            }
        }


        public Cursor Cursor { get; set; }
        public ViewModel View { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public LevelInfo CurrentLevel { get; set; }
        public List<Teleport> Teleports { get; set; }
        public bool Paused {
            get => _isPaused;
            set {
                _isPaused = value;
                if (value)
                    MainController.BreakTimers();
                else
                    MainController.EnableTimers();
            }
        }

        public virtual int EnergyCounter { get; set; }
        public virtual int Timer {
            get => _timer;
            set {
                if (value >= 0) {
                    _timer = value;
                    View?.DrawStatePanel();
                }
            }
        }

        public bool IsOnFrame(Point p) {
            return p.X == 0 || p.Y == 0 || p.X == W - 1 || p.Y == H - 1;
        }

        public virtual void ExecuteAction(Keys key) {
            switch (key) {
                case Keys.Up:
                    Cursor.MoveUp();
                    break;
                case Keys.Down:
                    Cursor.MoveDown();
                    break;
                case Keys.Left:
                    Cursor.MoveLeft();
                    break;
                case Keys.Right:
                    Cursor.MoveRight();
                    break;
            }
        }

        public virtual void ExecuteAction(ConsoleKey key) {
            switch (key) {
                case ConsoleKey.UpArrow:
                    Cursor.MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    Cursor.MoveDown();
                    break;
                case ConsoleKey.LeftArrow:
                    Cursor.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    Cursor.MoveRight();
                    break;
            }
        }

        public virtual void Resume() {
            View.Draw();
            Paused = false;
        }

        public abstract void Step();

        protected void GenerateCells() {
            Teleport.ResetColor();
            GameObjects = new GameObject[W, H];
            Teleports = CurrentLevel.Teleports;

            for (int i = 0; i < W; i++) {
                for (int j = 0; j < H; j++) {
                    this[i, j] = GetObjectByChar(CurrentLevel.Chars, i, j);
                }
            }
        }

        private GameObject GetObjectByChar(char[,] chars, int x, int y) {
            switch (chars[x, y]) {
                case Wall.DEFAULT_CHAR:
                    return new Wall();
                case EnergyBall.DEFAULT_CHAR:
                    return new EnergyBall();
                case LeftShield.DEFAULT_CHAR:
                    return new LeftShield();
                case RightShield.DEFAULT_CHAR:
                    return new RightShield();
                case Trap.DEFAULT_CHAR:
                    return new Trap();
                case Teleport.DEFAULT_CHAR:
                    return GetTeleport(x, y);
                case '\0':
                    return new Blank();
                default:
                    throw new ArgumentException($"Unable to recognize the character ({chars[x, y]}:level {CurrentLevel})");
            }
        }

        private Teleport GetTeleport(int x, int y) {
            foreach (var t in Teleports) {
                if (t.HasPoint(new Point(x, y))) {
                    Teleport.RefreshColor();
                    return t;
                }
            }
            throw new ArgumentException();
        }

        private bool IsLocationCorrect(int x, int y) {
            return x >= 0 && x < W && y >= 0 && y < H;
        }

    }
}
