using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame {
    static class Level {
        public static char[,] LoadLvl(int lvl) {
            StreamReader sr = new StreamReader($@"Levels\{lvl}.txt");
            using(sr) {
                int col = Convert.ToInt32(sr.ReadLine());
                int row = Convert.ToInt32(sr.ReadLine());

                char[,] chars = new char[row, col];
                for (int i = 0; i < row; i++) {
                    string line = sr.ReadLine();
                    for (int j = 0; j < col; j++) {
                        chars[i, j] = line[j];
                    }
                }

                return chars;
            }
        }
    }
}
