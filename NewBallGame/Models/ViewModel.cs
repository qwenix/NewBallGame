using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame.Models {
    public abstract class ViewModel {
        public static readonly Size CellSize = new Size(12, 15);

        public PlayGround Model { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }

        public ViewModel() { }

        public ViewModel(PlayGround model) {
            Model = model;
        }

        public void Draw(Point p) {
            Draw(p.X, p.Y);
        }

        public void Draw(Point p1, Point p2) {
            Draw(p1.X, p1.Y);
            Draw(p2.X, p2.Y);
        }

        public abstract void Draw();
        public abstract void Draw(int x, int y);
        public abstract void ShowMessage(string s);
        public abstract void DrawStatePanel();
        public abstract void DrawHelpPanel();
    }
}
