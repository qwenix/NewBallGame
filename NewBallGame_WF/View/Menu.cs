using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NewBallGame.Data;
using NewBallGame_WF.Controllers;
using NewBallGame.Models;

namespace NewBallGame_WF.View {
    public class Menu : Panel {
        private MainForm _form;

        public Menu(MainForm viewForm, MenuMode mode) {
            Parent = _form = viewForm;
            BackColor = Color.Black;
            Anchor = AnchorStyles.None;
            AutoSize = true;
            Dock = DockStyle.Fill;
            AddHeader();
            Run(mode);
        }

        private void AddHeader() {
            Label title = new Label() {
                Parent = this,
                ForeColor = Color.FromArgb(0, 192, 0),
                Anchor = AnchorStyles.None,
                Font = new Font("Snap ITC", 35f),
                Text = "NEW BALL GAME",
                AutoSize = true,
            };
            title.Location = new Point((Parent.Width - title.Width) / 2, 50);
        }

        public void Run(MenuMode mode) {
            ClearMenu();
            switch (mode) {
                case MenuMode.Start:
                    RunStartMenu();
                    break;
                case MenuMode.Pause:
                    RunPauseMenu();
                    break;
                case MenuMode.Gamemode:
                    RunGamemodeMenu();
                    break;
            }
            BringToFront();
        }

        private void RunPauseMenu() {
            Controls.Add(new MenuLabel("RESUME", 170, MenuButton.Resume));
            Controls.Add(new MenuLabel("RESTART", 240, MenuButton.Restart));
            Controls.Add(new MenuLabel("MENU", 380, MenuButton.Menu));
        }

        private void RunStartMenu() {
            Controls.Add(new MenuLabel("PLAY", 170, MenuButton.Play));
            Controls.Add(new MenuLabel("EDIT", 240, MenuButton.Edit));
            Controls.Add(new MenuLabel("EXIT", 380, MenuButton.Exit));
        }

        private void RunGamemodeMenu() {
            Controls.Add(new MenuLabel("NORMAL MODE", 170, MenuButton.NormalMode));
            Controls.Add(new MenuLabel("AUTO MODE", 240, MenuButton.AIMode));
        }

        private void ClearMenu() {
            for (int i = 0; i < Controls.Count; i++) {
                if (Controls[i] is MenuLabel) {
                    Controls.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ExecuteAction(MenuLabel button) {
            switch (button.ButtonType) {
                case MenuButton.Play:
                    Run(MenuMode.Gamemode);
                    break;
                case MenuButton.Level:
                    (Parent as MainForm).Start(new Game(int.Parse(button.Name), 0, 5, _form));
                    break;
                case MenuButton.Restart:
                    _form.RestartCurrent();
                    break;
                case MenuButton.Resume:
                    _form.ResumeCurrent();
                    break;
                case MenuButton.Menu:
                    _form.DisposeCurrent();
                    Run(MenuMode.Start);
                    break;
                case MenuButton.Edit:
                    RunLevelsMenu(MenuButton.EditorLevel);
                    break;
                case MenuButton.EditorLevel:
                    _form.Start(new Editor(int.Parse(button.Name), _form));
                    break;
                case MenuButton.New:
                    _form.Start(new Editor(_form));
                    break;
                case MenuButton.Exit:
                    Environment.Exit(0);
                    break;
                case MenuButton.AIMode:
                    RunLevelsMenu(MenuButton.AILevel);
                    break;
                case MenuButton.NormalMode:
                    RunLevelsMenu(MenuButton.Level);
                    break;
                case MenuButton.AILevel:
                    _form.Start(new AIGame(int.Parse(button.Name), 0, 5, _form));
                    break;
            }
        }

        private void RunLevelsMenu(MenuButton buttonType) {
            ClearMenu();
            int l = DataReader.GetLevelsCount();
            int i = 1;
            while (i <= l) {
                Controls.Add(new MenuLabel($"LEVEL {i:00}", 100 + 70 * i, buttonType) { Name = $"{i++}" } );
            }
            if (buttonType == MenuButton.EditorLevel) {
                Controls.Add(new MenuLabel("NEW", 100 + 70 * i++, MenuButton.New));
            }
            Controls.Add(new MenuLabel("BACK", 120 + 70 * i, MenuButton.Menu));
        }
    }
}
