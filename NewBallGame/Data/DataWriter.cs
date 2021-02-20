using NewBallGame.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Data {
    public class DataWriter {
        public static void AddLevel(LevelInfo info) {
            string serialized = JsonConvert.SerializeObject(info);
            using (StreamWriter sw = new StreamWriter($@"{DataPath.LEVEL_PATH}\{info.Id}.json")) {
                sw.Write(serialized);
            }
            RefreshLvlsList();
        }

        public static void RefreshLvlsList() {
            StreamWriter sw = new StreamWriter($@"{DataPath.MENU_ITEMS_PATH}\Lvlslistmenu.cmi", false, Encoding.Unicode);

            using (sw) {
                int l = DataReader.GetLevelsCount();
                int padding = 12;
                sw.WriteLine(l + 1);

                for (int i = 1; i <= l; i++) {
                    sw.WriteLine(3);
                    sw.WriteLine(10);
                    sw.WriteLine(padding);
                    padding = 0;
                    sw.WriteLine((int)MenuButton.Level);

                    sw.WriteLine(new string(' ', 10));
                    sw.WriteLine($"  LVL {i:00}  ");
                    sw.WriteLine(new string(' ', 10));
                }

                sw.WriteLine(3);
                sw.WriteLine(10);
                sw.WriteLine(padding);
                sw.WriteLine((int)MenuButton.New);

                sw.Write(new string(' ', 10) + "\n   NEW    \n" + new string(' ', 10));
            }
        }
    }
}
