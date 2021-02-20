using System;
using System.Drawing;
using NewBallGame.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewBallGame.Models.Game_objects {
    public abstract class GameObject {
        public char Char { get; protected set; }
        public ObjectColor ForeColor { get; protected set; }
        public ObjectColor BackColor { get; set; }

        public GameObject(char c, ObjectColor color) {
            Char = c;
            ForeColor = color;
            BackColor = new ObjectColor(Console.BackgroundColor);
        }

        public GameObject() { }

        public override string ToString() {
            return Char.ToString();
        }
    }
}
