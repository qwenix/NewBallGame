using NewBallGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewBallGame.Models {
    public enum MenuButton {
        Title,
        Play,
        Resume,
        Edit,
        Exit,
        Restart,
        Menu,
        Level,
        New,
        EditorLevel,
        NormalMode,
        AIMode,
        AILevel
    }

    public enum MenuMode { Start, Pause, LvlsList, EditorLvlsList, AILvlsList, Gamemode }

    public interface IMenu {
        IPlayableController MainController { get; set; }
        void Run(MenuMode mode);
    }
}
