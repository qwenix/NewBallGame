using NewBallGame.Models;
using NewBallGame_WF.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame_WF.View {
    class GameView : FormView {
        public GameView(PlayGround model, MainForm view, KeyController controller) : 
            base(model, view, controller) { }

        public override void DrawHelpPanel() {
            //string text = $"\\ - press Z   / - press X   Remove - press 'Space'   ▲ ► ▼ ◄ - move cursor";

            //Bitmap buffer = new Bitmap(text.Length * CellSize.Width + GetFontOffset(text), CellSize.Height);
            //Graphics bg = Graphics.FromImage(buffer);
            //bg.DrawString(text, GamePanel.Font, Brushes.LightGreen, 0, 0);

            //Graphics g = GamePanel.CreateGraphics();
            //g.DrawImageUnscaled(buffer, (GamePanel.Width - buffer.Width) / 2, helpPanelTop);
        }

        public override void DrawStatePanel() {
            string text = $"SCORE: {Model.Score:00000}    ENERGY: {Model.EnergyCounter:000}    TIME: {Model.Timer / 60}:{Model.Timer % 60:00}    LIVES:{Model.Lives}";

            Bitmap buffer = new Bitmap(text.Length * (CellSize.Width - 4), CellSize.Height * 2);
            Graphics bg = Graphics.FromImage(buffer);
            bg.FillRectangle(Brushes.Black, new Rectangle(0, 0, buffer.Width, buffer.Height));
            bg.DrawString(text, GamePanel.Font, Brushes.BlueViolet, 0, 0);

            Graphics g = GamePanel.CreateGraphics();
            g.DrawImageUnscaled(buffer, (GamePanel.Width - buffer.Width) / 2, statePanelTop);
        }
    }
}
