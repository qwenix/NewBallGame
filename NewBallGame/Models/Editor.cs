using NewBallGame.Data;
using NewBallGame.Models.Game_objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewBallGame.Models {
    public class Editor : PlayGround {
        public Editor(IPlayableController mainController) {
            Teleports = new List<Teleport>();
            MainController = mainController;

            GenerateDefault();
            Cursor = new Cursor(this);
        }

        public Editor(int lvl, IPlayableController mainController) {
            CurrentLevel = DataReader.GetLevelInfo(lvl);
            Timer = CurrentLevel.Time;
            EnergyCounter = CurrentLevel.EnergyBallNumber;
            MainController = mainController;

            GenerateCells();

            Cursor = new Cursor(this);
        }

        public override void ExecuteAction(Keys key) {
            switch (key) {
                case Keys.W:
                    Add(new Wall());
                    break;
                case Keys.E:
                    Add(new EnergyBall());
                    break;
                case Keys.T:
                    Add(new Trap());
                    break;
                case Keys.P:
                    AddTeleport();
                    break;
                case Keys.Space:
                    Remove();
                    break;
                case Keys.Enter:
                    Save();
                    break;
                case Keys.A:
                    Timer -= 5;
                    break;
                case Keys.D:
                    Timer += 5;
                    break;
                case Keys.Escape:
                    MainController.ShowMenu(MenuMode.Start);
                    MainController.DisposeCurrent();
                    break;
                default:
                    base.ExecuteAction(key);
                    break;
            }
        }

        public override void ExecuteAction(ConsoleKey key) {
            switch (key) {
                case ConsoleKey.W:
                    Add(new Wall());
                    break;
                case ConsoleKey.E:
                    Add(new EnergyBall());
                    break;
                case ConsoleKey.T:
                    Add(new Trap());
                    break;
                case ConsoleKey.P:
                    AddTeleport();
                    break;
                case ConsoleKey.Spacebar:
                    Remove();
                    break;
                case ConsoleKey.Enter:
                    Save();
                    break;
                case ConsoleKey.A:
                    Timer -= 5;
                    break;
                case ConsoleKey.D:
                    Timer += 5;
                    break;
                case ConsoleKey.Escape:
                    MainController.ShowMenu(MenuMode.Start);
                    MainController.DisposeCurrent();
                    break;
                default:
                    base.ExecuteAction(key);
                    break;
            }
        }

        private void Save() {
            Paused = true;
            if (!TrySave())
                View.ShowMessage("Timer or Energy is null, or not all Teleports added!");
            else
                View.ShowMessage("The level successfully added!");

        }

        //Возвращает true, если удалось сохранить, иначе false.
        private bool TrySave() {
            if (!IsLevelCorrect())
                return false;

            LevelInfo info = new LevelInfo() {
                Id = CurrentLevel.Id == 0 ? DataReader.GetLevelsCount() + 1 : CurrentLevel.Id,
                EnergyBallNumber = EnergyCounter,
                Time = Timer,
                Teleports = this.Teleports,
                Chars = new char[W, H]
            };

            for (int i = 0; i < W; i++) {
                for (int j = 0; j < H; j++) {
                    info.Chars[i, j] = GameObjects[i, j].Char;
                }
            }

            DataWriter.AddLevel(info);
            return true;
        }

        protected bool IsLevelCorrect() {
            if (EnergyCounter == 0 || Timer == 0)
                return false;
            foreach (Teleport t in Teleports) {
                if (!t.IsTeleport())
                    return false;
            }
            return true;
        }

        protected virtual void Remove() {
            GameObject obj = this[Cursor.Position.X, Cursor.Position.Y];
            if (obj is Blank || IsOnFrame(Cursor.Position))
                return;
            if (obj is EnergyBall)
                EnergyCounter--;
            if (obj is Teleport) {
                RemoveTeleport();
            }

            this[Cursor.Position.X, Cursor.Position.Y] = new Blank() { BackColor = new ObjectColor(Cursor.DEFAULT_COLOR) };
            View.Draw(Cursor.Position);
        }

        protected void RemoveTeleport() {
            foreach (var t in Teleports) {
                if (t.HasPoint(Cursor.Position)) {
                    t.RemoveExitPoint(Cursor.Position);
                    if (!t.HasAnyPoint())
                        Teleports.Remove(t);
                    return;
                }
            }
        }

        protected void AddTeleport() {
            if (this[Cursor.Position.X, Cursor.Position.Y] is Blank) {
                foreach (var t in Teleports) {
                    if (!t.IsTeleport()) {
                        t.AddExitPoint(Cursor.Position);
                        Add(t);
                        return;
                    }
                }
                var tel = new Teleport(Cursor.Position);
                Teleports.Add(tel);
                Add(tel);
            }
        }

        protected virtual void Add(GameObject obj) {
            if (this[Cursor.Position.X, Cursor.Position.Y] is Blank) {
                if (obj is EnergyBall)
                    EnergyCounter++;

                obj.BackColor = new ObjectColor(Cursor.DEFAULT_COLOR);
                this[Cursor.Position.X, Cursor.Position.Y] = obj;
                View.Draw(Cursor.Position);
            }
        }

        private void GenerateDefault() {
            GameObjects = new GameObject[W, H];

            for (int i = 0; i < W; i++) {
                for (int j = 0; j < H; j++) {
                    if (i == 0 || j == 0 || i == W - 1 || j == H - 1) {
                        GameObjects[i, j] = new Wall();
                    }
                    else
                        GameObjects[i, j] = new Blank();
                }
            }
        }

        public override void Step() {
            throw new NotImplementedException();
        }
    }
}
