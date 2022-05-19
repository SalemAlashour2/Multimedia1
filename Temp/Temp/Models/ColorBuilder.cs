using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageProcessing
{
    public class ColorBuilder
    {
        private static Color GetNearestLeftColor(Color c)
        {
            if ((c.R <= c.G) & (c.R <= c.B))
            {
                return Color.FromArgb(0, c.G, c.B);
            }
            if ((c.G <= c.R) & (c.G <= c.B))
            {
                return Color.FromArgb(c.R, 0, c.B);
            }
            if ((c.B <= c.G) & (c.B <= c.R))
            {
                return Color.FromArgb(c.R, c.G, 0);
            }
            return c;
        }

        private static Color GetNearestRigthColor(Color c)
        {
            if ((c.R >= c.G) & (c.R >= c.B))
            {
                return Color.FromArgb(255, c.G, c.B);
            }
            if ((c.G >= c.R) & (c.G >= c.B))
            {
                return Color.FromArgb(c.R, 255, c.B);
            }
            if ((c.B >= c.G) & (c.B >= c.R))
            {
                return Color.FromArgb(c.R, c.G, 255);
            }
            return c;
        }

        public static Color[] GetColorDiagram(List<ControlPoint> points)
        {
            Color[] colors = new Color[256];
            points.Sort(new PointsComparer());

            for (int i = 0; i < 256; i++)
            {
                ControlPoint leftColor = new ControlPoint(0, GetNearestLeftColor(points[0].Color)); //new ControlPoint(0, Color.Black);
                ControlPoint rightColor = new ControlPoint(255, GetNearestRigthColor(points[points.Count - 1].Color));
                if (i < points[0].Level)
                {
                    rightColor = points[0];
                }
                if (i > points[points.Count - 1].Level)
                {
                    leftColor = points[points.Count - 1];
                }
                else
                {
                    for (int j = 0; j < points.Count - 1; j++)
                    {
                        if ((points[j].Level <= i) & (points[j + 1].Level > i))
                        {
                            leftColor = points[j];
                            rightColor = points[j + 1];
                        }
                    }
                }
                if ((rightColor.Level - leftColor.Level) != 0)
                {
                    double koef = (double)(i - leftColor.Level) / (double)(rightColor.Level - leftColor.Level);
                    int r = leftColor.Color.R + (int)(koef * (rightColor.Color.R - leftColor.Color.R));
                    int g = leftColor.Color.G + (int)(koef * (rightColor.Color.G - leftColor.Color.G));
                    int b = leftColor.Color.B + (int)(koef * (rightColor.Color.B - leftColor.Color.B));
                    colors[i] = Color.FromArgb(r, g, b);
                }
                else
                {
                    colors[i] = leftColor.Color;
                }

            }

            return colors;
        }
    }
}
