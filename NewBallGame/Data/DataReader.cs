using System.IO;
using Newtonsoft.Json;
using System.Text;
using NewBallGame.View;
using System.Collections.Generic;
using System;
using NewBallGame.Models;
using System.Drawing;

namespace NewBallGame.Data {
    struct DataPath {
        public const string LEVEL_PATH = @"..\..\..\data\Levels";
        public const string MENU_ITEMS_PATH = @"..\..\..\data\Menu items";
        public const string IMAGE_PATH = @"Images";
    }

    public static class DataReader {
        static public int GetLevelsCount() {
            return Directory.GetFiles(DataPath.LEVEL_PATH).Length - 1;
        }

        static public Image GetImage(string name) {
            return new Bitmap($@"{DataPath.IMAGE_PATH}\{name}");
        }

        public static LevelInfo GetLevelInfo(int lvl) {
            if (GetLevelsCount() < lvl)
                lvl = 1;

            using (StreamReader sr = new StreamReader($@"{DataPath.LEVEL_PATH}\{lvl}.json")) {
                return JsonConvert.DeserializeObject<LevelInfo>(sr.ReadLine());
            }
        }

        public static MenuItem[] GetMenuItems(MenuMode type) {
            StreamReader sr = new StreamReader($@"{DataPath.MENU_ITEMS_PATH}\{type}menu.cmi");
            MenuItem[] items;
            using (sr) {
                int l = int.Parse(sr.ReadLine());
                items = new MenuItem[l];
                int top = 0;

                for (int i = 0; i < l; i++) {
                    int h = int.Parse(sr.ReadLine());
                    int w = int.Parse(sr.ReadLine());
                    int paddingTop = int.Parse(sr.ReadLine());
                    int button = int.Parse(sr.ReadLine());

                    string[] text = new string[h];
                    for (int k = 0; k < h; k++) {
                        text[k] = sr.ReadLine();
                    }

                    top += paddingTop;
                    items[i] = new MenuItem(text, ConsoleView.WINDOW_WIDTH / 2 - w / 2, top, button);
                    top = (i + 1) % Menu.MAX_ITEMS == 0 ? 9 : top + h;
                }
            }
            return items;
        }
    }
}
