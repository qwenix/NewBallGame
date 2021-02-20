using NewBallGame.Models.Game_objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models {
    public class AIGame : Game {
        public AIGame(IPlayableController mainController) : base(mainController) { }

        public AIGame(int lvl, int score, int lives, IPlayableController mainController) :
            base(lvl, score, lives, mainController) { }

        protected override void AddBall() {
            MainBall = new AutoBall(this) { Position = new Point(1, 1) };
            this[1, 1] = MainBall;
        }

        public override void Resume() {
            if (EnergyCounter == 0)
                MainController.Start(new AIGame(CurrentLevel.Id + 1, Score, Lives, MainController));
            else if (Timer != 0)
                base.Resume();
            else
                Restart();
        }
    }
}
