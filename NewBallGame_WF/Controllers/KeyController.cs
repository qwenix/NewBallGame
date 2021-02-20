using NewBallGame;
using NewBallGame.Models;
using NewBallGame_WF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace NewBallGame_WF.Controllers {
    class KeyController : Button {
        public PlayGround Model { get; set; }

        public KeyController(PlayGround model) {
            Model = model;
            Width = 0;
        }

        public KeyController() { }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e) {
            base.OnPreviewKeyDown(e);
            if (!Model.Paused)
                Model.ExecuteAction(e.KeyCode);
            else if (e.KeyCode == Keys.Enter) {
                Model.MainController.ResumeCurrent();
            }
        }
    }
}
