using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NewBallGame.Models;
using NewBallGame.Controllers;

namespace NewBallGame.View {
    public abstract class ConsoleView : ViewModel {
        public const int WINDOW_WIDTH = 90;
        public const int WINDOW_HEIGHT = 42;

        public ConsoleView(PlayGround model) : base(model) {
            Left = WINDOW_WIDTH / 2 - PlayGround.W / 2;
            Top = 3;

            Draw();
        }

        public override void Draw() {
            Console.Clear();

            for (int i = 0; i < PlayGround.W; i++) {
                for (int j = 0; j < PlayGround.H; j++) {
                    SetCursorPosition(i, j);
                    Console.ForegroundColor = Model[i, j].ForeColor.ConsoleColor;
                    Console.BackgroundColor = Model[i, j].BackColor.ConsoleColor;
                    Console.Write(Model[i, j]);
                }
            }
            DrawStatePanel();
            DrawHelpPanel();
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void ShowMessage(string s) {
            string str = "(press 'Enter' to continue)";
            string space = new string(' ', Math.Max(s.Length, str.Length) + 4);
            string sSpace = new string(' ', (space.Length - s.Length) / 2);
            string strSpace = new string(' ', (space.Length - str.Length) / 2);

            string[] message = {
                space,
                sSpace + s + new string(' ', space.Length - sSpace.Length - s.Length),
                strSpace + str + new string(' ', space.Length - strSpace.Length - str.Length),
                space
            };

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < message.Length; i++) {
                SetCursorPosition(PlayGround.W / 2 - space.Length / 2, PlayGround.H / 2 - 2 + i);
                Console.Write(message[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void Draw(int x, int y) {
            SetCursorPosition(x, y);
            Console.ForegroundColor = Model[x, y].ForeColor.ConsoleColor;
            Console.BackgroundColor = Model[x, y].BackColor.ConsoleColor;
            Console.Write(Model[x, y]);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void PrepareConsole() {
            Console.Title = "New Ball Game";
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WindowWidth = WINDOW_WIDTH;
            Console.WindowHeight = WINDOW_HEIGHT;
            Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        private void SetCursorPosition(int x, int y) {
            Console.SetCursorPosition(Left + x, Top + y);
        }

        public override void DrawStatePanel() { }
        public override void DrawHelpPanel() { }
    }
}
