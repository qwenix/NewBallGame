using NewBallGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models {
    public interface IPlayableController {
        PlayGround Model { get; set; }

        void Start(Game game);
        void Start(Editor editor);
        void ShowMenu(MenuMode mode);
        void BreakTimers();
        void EnableTimers();
        void DisposeCurrent();
        void ResumeCurrent();
        void RestartCurrent();
    }
}
