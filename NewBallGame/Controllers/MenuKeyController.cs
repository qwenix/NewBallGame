using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewBallGame.Models;
using NewBallGame.View;
using Menu = NewBallGame.View.Menu;

namespace NewBallGame.Controllers {
    class MenuKeyController : KeyController {
        private Menu _menu;

        public MenuKeyController(Menu menu) {
            _menu = menu;
            Enable();
        }

        public override void Enable() {
            while (true) {
                if (Console.KeyAvailable) {
                    _menu.ExecuteAction(Console.ReadKey(true).Key);
                }
            }
        }
    }
}
