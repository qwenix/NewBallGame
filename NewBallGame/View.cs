using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame {
    static class View {
        public static void PrepareConsole() {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
        }

        public static void Draw(Cell[,] cells) {
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    Console.SetCursorPosition(j, i);
                    Console.ForegroundColor = cells[i, j].FgColor;
                    Console.BackgroundColor = cells[i, j].BgColor;
                    Console.Write(cells[i, j].Char);
                }
            }
        }

        public static void Draw(Cell[,] cells, int i, int j) {
            Console.SetCursorPosition(j, i);
            Console.ForegroundColor = cells[i, j].FgColor;
            Console.BackgroundColor = cells[i, j].BgColor;
            Console.Write(cells[i, j].Char);
        }
    }
}
