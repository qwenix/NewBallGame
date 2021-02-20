using NewBallGame.Controllers;
using NewBallGame.Data;
using NewBallGame.Models;
using NewBallGame.View;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace NewBallGame {
    class Program {
        public static IPlayableController MainController;

        static void Main(string[] args) {
            ConsoleView.PrepareConsole();
            DataWriter.RefreshLvlsList();
            MainController = new MainController();
        }
    }
}