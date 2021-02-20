using NewBallGame.Data;
using NewBallGame.Models;
using NewBallGame.Models.Game_objects;
using NewBallGame_WF.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewBallGame_WF.View {
    abstract class FormView : ViewModel {
        public const int TOP = 65;
        private const int MESSAGE_PADDING = 20;

        protected readonly int fontOffset;
        protected readonly int statePanelTop;
        protected readonly int helpPanelTop;

        private string _currentMessageText;

        public KeyController Controller { get; set; }
        public GamePanel GamePanel { get; set; }
        public Size PlayGroundSize { get; }

        public FormView(PlayGround model, MainForm viewForm, KeyController controller) {
            GamePanel = new GamePanel(viewForm, this);
            GamePanel.Controls.Add(Controller = controller);
            controller.Focus();

            Model = model;
            PlayGroundSize = new Size(PlayGround.W * CellSize.Width, PlayGround.H * CellSize.Height);
            fontOffset = CellSize.Width * 2 / 9;
            statePanelTop = (TOP - CellSize.Height) / 2;
            helpPanelTop = TOP + PlayGroundSize.Height + statePanelTop;

            InitializeImages();
        }

        public override void Draw() {
            //Создаём буффер.
            Bitmap buffer = new Bitmap(PlayGroundSize.Width, PlayGroundSize.Height);
            Graphics bg = Graphics.FromImage(buffer);

            for (int i = 0; i < PlayGround.W; i++) {
                for (int j = 0; j < PlayGround.H; j++) {
                    Image img = GetImage(Model[i, j]);
                    if (img != null)
                        bg.DrawImage(img, i * CellSize.Width, j * CellSize.Height, CellSize.Width, CellSize.Height);
                    else {
                        bg.FillRectangle(new SolidBrush(Model[i, j].BackColor.FormColor), i * CellSize.Width, j * CellSize.Height, CellSize.Width, CellSize.Height);

                        SolidBrush brush = new SolidBrush(Model[i, j].ForeColor.FormColor);
                        bg.DrawString(Model[i, j].ToString(), GamePanel.Font, brush, (i * CellSize.Width) - fontOffset, j * CellSize.Height);
                    }
                }
            }

            //Переносим содержимое буфера на экран.
            Graphics g = GamePanel.CreateGraphics();
            g.DrawImageUnscaled(buffer, new Point((GamePanel.Width - buffer.Width) / 2, TOP));
        }

        public override void Draw(int x, int y) {
            if (Model[x, y] is Wall && Model is Game) return;
            Bitmap buffer = new Bitmap(CellSize.Width, CellSize.Height);
            Graphics bg = Graphics.FromImage(buffer);
            bg.FillRectangle(new SolidBrush(Model[x, y].BackColor.FormColor), 0, 0, buffer.Width, buffer.Height);

            Image img = GetImage(Model[x, y]);
            if (img != null) {
                bg.DrawImage(img, 0, 0);
            }
            else {
                SolidBrush brush = new SolidBrush(Model[x, y].ForeColor.FormColor);
                bg.DrawString(Model[x, y].ToString(), GamePanel.Font, brush, -fontOffset, 0);
            }

            Graphics g = GamePanel.CreateGraphics();
            int _x = (GamePanel.Width - PlayGroundSize.Width) / 2 + x * CellSize.Width;
            int _y = TOP + y * CellSize.Height;
            g.DrawImageUnscaled(buffer, new Point(_x, _y));
        }

        public override void ShowMessage(string s) {
            _currentMessageText = s;
            int offset = GetFontOffset(s);
            int w = s.Length * (CellSize.Width - 1) + 2 * MESSAGE_PADDING;
            int h = CellSize.Height + 2 * MESSAGE_PADDING;

            Bitmap buffer = new Bitmap(w, h);
            Graphics bg = Graphics.FromImage(buffer);
            bg.FillRectangle(new SolidBrush(Color.LightGray), 0, 0, buffer.Width, buffer.Height);

            SolidBrush brush = new SolidBrush(Color.Black);
            bg.DrawString(s, GamePanel.Font, brush, MESSAGE_PADDING - fontOffset, MESSAGE_PADDING);

            Graphics g = GamePanel.CreateGraphics();
            int _x = (GamePanel.Width - buffer.Width) / 2;
            int _y = (GamePanel.Height - buffer.Height) / 2;
            g.DrawImageUnscaled(buffer, new Point(_x, _y));
        }

        public void DrawMessageBox() {
            if (_currentMessageText != null) {
                ShowMessage(_currentMessageText);
            }
        }

        public void HideMessage() {
            _currentMessageText = null;
        } 

        public override void DrawStatePanel() { }
        public override void DrawHelpPanel() { }

        protected int GetFontOffset(string s) {
            return s.Length / CellSize.Width;
        }

        private Image GetImage(GameObject obj) {
            if (obj is Wall)
                return _wallImage;
            else if (obj is Ball)
                return _ballImage;
            else if (obj is EnergyBall)
                return _energyImage;
            else if (obj is LeftShield)
                return _leftShieldImage;
            else if (obj is RightShield)
                return _rightShieldImage;
            else
                return null;
        }

        private void InitializeImages() {
            _wallImage = DataReader.GetImage("Wall_sm.jpg");

            InitializeImage("Ball");
            InitializeImage("Energy");

            Image img = new Bitmap(CellSize.Width, CellSize.Height);
            Graphics g = Graphics.FromImage(img);
            g.DrawLine(new Pen(Brushes.White, 2), new Point(0, 0), new Point(img.Width, img.Height));
            _leftShieldImage = img;

            img = new Bitmap(CellSize.Width, CellSize.Height);
            g = Graphics.FromImage(img);
            g.DrawLine(new Pen(Brushes.White, 2), new Point(img.Width, 0), new Point(0, img.Height));
            _rightShieldImage = img;
        }

        private void InitializeImage(string type) {
            int w = CellSize.Width;
            int h = CellSize.Height;
            int offset = 0;

            if (type == "Ball") {
                w -= 4;
                h -= 5;
                offset = 2;
            }

            Image img = new Bitmap(CellSize.Width, CellSize.Height);
            Graphics g = Graphics.FromImage(img);

            g.FillEllipse(offset == 0 ? Brushes.Yellow : Brushes.Green, offset, (CellSize.Height - w) / 2, w, w);

            if (offset == 0)
                _energyImage = img;
            else
                _ballImage = img;
        }

        private Image _ballImage;
        private Image _wallImage;
        private Image _energyImage;
        private Image _leftShieldImage;
        private Image _rightShieldImage;
    }
}
