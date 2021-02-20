using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewBallGame.Models;
using NewBallGame_WF.View;
using Menu = NewBallGame_WF.View.Menu;

namespace NewBallGame_WF.Controllers {
    public class MenuLabel : Label {
        private readonly int top;

        public MenuButton ButtonType { get; set; }

        public MenuLabel(string text, int top, MenuButton button) {
            this.top = top;
            ButtonType = button;
            AutoSize = true;
            Font = new Font("Snap ITC", 30);
            ForeColor = Color.FromArgb(224, 224, 224);
            Anchor = AnchorStyles.None;
            Text = text;
        }

        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);
            if (Parent != null) {
                Location = new Point((Parent.Width - Width) / 2, top);
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            BackColor = Color.Gray;
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            if (Parent != null) {
                BackColor = Parent.BackColor;
            }
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
            Menu m = Parent as Menu;
            m?.ExecuteAction(this);
        }
    }
}
