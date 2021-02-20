using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewBallGame.Data;
using NewBallGame.Models;
using Menu = NewBallGame_WF.View.Menu;
using NewBallGame;
using NewBallGame_WF.View;
using NewBallGame_WF.Controllers;

namespace NewBallGame_WF {
    public partial class MainForm : Form, IPlayableController {
        public Menu GameMenu { get; set; }
        public PlayGround Model { get; set; }

        public MainForm() {
            InitializeComponent();
            ShowMenu(MenuMode.Start);
        }

        public void Start(Game game) {
            DisposeCurrent();
            Model = game;
            Model.View = new GameView(Model, this, new KeyController(Model));
            BallMovementTimer.Interval = Game.TIMER_INTERVAL;
            EnableTimers();
        }

        public void Start(Editor editor) {
            DisposeCurrent();
            Model = editor;
            Model.View = new EditorView(editor, this, new KeyController(editor));
        }

        public void RestartCurrent() {
            DisposeCurrent();
            Game g = Model as Game;
            Model = new Game(g.CurrentLevel.Id, g.StartScore, g.Lives, this);
            Model.View = new GameView(Model, this, new KeyController(Model));
            EnableTimers();
        }

        public void GameTimer_Tick(object sender, EventArgs e) {
            Model.Timer--;
        }

        public void BallMovement_Tick(object sender, EventArgs e) {
            Model.Step();
        }

        public void BreakTimers() {
            BallMovementTimer.Enabled = false;
            GameTimer.Enabled = false;
        }

        public void EnableTimers() {
            if (Model is Game) {
                BallMovementTimer.Enabled = true;
                GameTimer.Enabled = true;
            }
        }

        public void ShowMenu(MenuMode mode) {
            GameMenu = new Menu(this, mode);
        }

        public void DisposeCurrent() {
            if (Model != null) {
                (Model.View as FormView).GamePanel.Parent = null;
            }
        }

        public void ResumeCurrent() {
            Model.Resume();
            (Model.View as FormView).GamePanel.BringToFront();
        }
    }
}
