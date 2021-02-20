using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewBallGame.Models;
using NewBallGame_WF.Controllers;
using NewBallGame_WF.View;
using System.Drawing;

namespace NewBallGame_WF.View {
    class EditorView : FormView {
        public EditorView(PlayGround model, MainForm view, KeyController controller) :
            base(model, view, controller) { }

        public override void DrawHelpPanel() {
            //string text = $"W - wall   E - energy ball   T - trap   P - teleport   'Space' - remove";

            //Bitmap buffer = new Bitmap(text.Length * CellSize.Width + GetFontOffset(text), CellSize.Height);
            //Graphics bg = Graphics.FromImage(buffer);
            //bg.DrawString(text, GamePanel.Font, Brushes.LightGreen, 0, 0);

            //Graphics g = GamePanel.CreateGraphics();
            //g.DrawImageUnscaled(buffer, (GamePanel.Width - buffer.Width) / 2, helpPanelTop);
        }

        public override void DrawStatePanel() {
            string text_1 = $"Timer: {Model.Timer / 60}:{Model.Timer % 60:00}   A - reduce 5s   D - add 5s";

            int l1 = text_1.Length * ((int)GamePanel.Font.Size * 3/4);

            Bitmap buffer = new Bitmap(l1, CellSize.Height * 2);
            Graphics bg = Graphics.FromImage(buffer);
            bg.FillRectangle(Brushes.Black, new Rectangle(0, 0, buffer.Width, buffer.Height));
            bg.DrawString(text_1, GamePanel.Font, Brushes.AliceBlue, 0, 0);

            Graphics g = GamePanel.CreateGraphics();
            g.DrawImageUnscaled(buffer, (GamePanel.Width - buffer.Width) / 2, statePanelTop);
        }
    }
}
