using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewBallGame.Models;
using NewBallGame.View;
using Menu = NewBallGame.View.Menu;

namespace NewBallGame.Controllers {
    class MainController : IPlayableController {
        public Menu GameMenu { get; set; }
        public PlayGround Model { get; set; }
        public KeyController KeyController { get; set; }


        public MainController() {
            ShowMenu(MenuMode.Start);
        }

        public void Start(Game game) {
            DisableKeyController();
            Model = game;
            Model.View = new GameView(game);
            KeyController = new KeyController(game);

            EnableTimers();
        }

        public void Start(Editor editor) {
            DisableKeyController();
            Model = editor;
            Model.View = new EditorView(editor);
            KeyController = new KeyController(editor);

            EnableTimers();
        }

        public void RestartCurrent() {
            DisableKeyController();
            if (Model is Game g)
                Start(new Game(g.CurrentLevel.Id, g.StartScore, g.Lives, this));
        }

        public void EnableTimers() {
            KeyController?.Enable();
        }
        public void BreakTimers() { }

        public void ShowMenu(MenuMode mode) {
            GameMenu = new Menu(this, mode);
        }

        public void DisposeCurrent() {
            Model = null;
        }

        public void ResumeCurrent() {
            Model.Resume();
        }

        private void DisableKeyController() {
            if (KeyController != null) {
                KeyController.IsEnabled = false;
            }
        }
    }
}
