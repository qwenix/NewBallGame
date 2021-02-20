using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using NewBallGame.Models;

namespace NewBallGame.Controllers {
    class KeyController {
        public bool IsEnabled { get; set; }

        private PlayGround _model { get; set; }

        public KeyController() { }

        public KeyController(PlayGround model) {
            _model = model;
            IsEnabled = true;
        }

        public virtual void Enable() {
            IsEnabled = true;
            var ball_sw = new Stopwatch();
            var time_sw = new Stopwatch();
            ball_sw.Start();
            time_sw.Start();

            while (IsEnabled) {
                if (!_model.Paused && _model is Game) {
                    if (ball_sw.ElapsedMilliseconds >= Game.TIMER_INTERVAL) {
                        _model.Step();
                        ball_sw.Restart();
                    }
                    if (time_sw.ElapsedMilliseconds >= 1000) {
                        _model.Timer--;
                        time_sw.Restart();
                    }
                }
                if (Console.KeyAvailable) {
                    if (!_model.Paused)
                        _model.ExecuteAction(Console.ReadKey(true).Key);
                    else if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                        _model.MainController.ResumeCurrent();
                    }
                }
            }
        }
    }
}
