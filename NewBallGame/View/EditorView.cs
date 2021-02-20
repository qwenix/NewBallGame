using NewBallGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.View {
    class EditorView : ConsoleView {
        public EditorView(PlayGround model) : base(model) { }

        public override void DrawHelpPanel() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string panel = $"W - wall   E - energy ball   T - trap   P - teleport   'Space' - remove";
            Console.SetCursorPosition(WINDOW_WIDTH / 2 - panel.Length / 2, 1);
            Console.WriteLine(panel);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void DrawStatePanel() {
            Console.ForegroundColor = ConsoleColor.Green;
            string panel = $"Timer: {Model.Timer / 60}:{Model.Timer % 60:00}   A - reduce 5s   D - add 5s";
            Console.SetCursorPosition(WINDOW_WIDTH / 2 - panel.Length / 2, Top + PlayGround.H + 1);
            Console.Write(panel);
            panel = "▲ ► ▼ ◄ - move cursor Enter - save";
            Console.SetCursorPosition(WINDOW_WIDTH / 2 - panel.Length / 2, Top + PlayGround.H + 2);
            Console.Write(panel);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
