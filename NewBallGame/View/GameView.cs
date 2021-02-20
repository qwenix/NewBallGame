using NewBallGame.Models;
using System;
using System.Text;

namespace NewBallGame.View {
    class GameView : ConsoleView {

        public GameView(PlayGround model) : base(model) { }

        public override void DrawHelpPanel() {
            string panel = "\\ - press Z   / - press X   Remove - press 'Space'   ▲ ► ▼ ◄ - move cursor";

            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.SetCursorPosition(WINDOW_WIDTH / 2 - panel.Length / 2, Top + PlayGround.H + 1);
            System.Console.Write(panel);
        }

        public override void DrawStatePanel() {
            string panel = $"SCORE: {Model.Score:00000}    ENERGY: {Model.EnergyCounter:000}    TIME: {Model.Timer / 60}:{Model.Timer % 60:00}    LIVES:{Model.Lives}";

            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.SetCursorPosition(WINDOW_WIDTH / 2 - panel.Length / 2, 1);
            System.Console.WriteLine(panel);
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
