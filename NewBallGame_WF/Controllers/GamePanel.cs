using NewBallGame.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewBallGame_WF.View;
using NewBallGame.Models.Game_objects;

namespace NewBallGame_WF.Controllers {
    class GamePanel : Panel {
        private ViewModel _view;

        public GamePanel(MainForm parent, FormView view) {
            _view = view;
            Parent = parent;
            AutoSize = true;
            Anchor = AnchorStyles.None;
            Dock = DockStyle.Fill;
            BackColor = Color.Black;
            Font = new Font("Raster fonts", ViewModel.CellSize.Width);
            BringToFront();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            _view.Draw();
            _view.DrawStatePanel();
            _view.DrawHelpPanel();
            (_view as FormView)?.DrawMessageBox();
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if (_view.Model.Paused) return;
            base.OnMouseMove(e);
            FormView view = _view as FormView;
            int x = (e.X - (Width / 2 - view.PlayGroundSize.Width / 2)) / ViewModel.CellSize.Width;
            int y = (e.Y - FormView.TOP) / ViewModel.CellSize.Height;

            Point p = new Point(x, y);
            if (_view.Model[x, y] != null && _view.Model.Cursor.Position != p)
                _view.Model.Cursor.Position = p;
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            Game g = _view.Model as Game;
            if (g == null) return;
            g.RemoveShield();
            if (e.Button == MouseButtons.Left)
                g.AddShield(new LeftShield());
            else if (e.Button == MouseButtons.Right)
                g.AddShield(new RightShield());
        }
    }
}
