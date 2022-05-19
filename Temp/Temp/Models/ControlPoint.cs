using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageProcessing
{
    public struct ControlPoint
    {
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public ControlPoint(int level, Color color)
        {
            this.color = color;
            this.level = level;
        }

        public override string ToString()
        {
            return "Level: " + level.ToString() + "; Color: " + color.ToString();
        }

        public string XMLString
        {
            get
            {
                return "<ControlPoint><Level>" + level.ToString() + "</Level><R>" +
                        color.R.ToString() + "</R><G>" + color.G.ToString() + "</G><B>" +
                        color.B.ToString() + "</B></ControlPoint>";
            }
        }
    }
}
