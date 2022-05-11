using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multimedia_ITE_HW_1
{
    public partial class Form1 : Form
    {

        Graphics graphics;

        int x = -1;
        int y = -1;

        bool isMove = false;

        Pen pen;
        public Form1()
        {
            InitializeComponent();

            graphics = pic.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;

            isMove = true;
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove && x != -1 && y != -1)
            {
                graphics.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }

            this.Text = "X :  " + e.X + "  Y : " + e.Y;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = true;
            x = -1;
            y = -1;
        }

        private void open_tsmi_Click(object sender, EventArgs e)
        {
            OpenImageFromDrive();
        }







        private void OpenImageFromDrive()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.JPG;*.PNG;*.GIF;*.JPEG|*.JPG;*.PNG;*.GIF;*.JPEG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic.Image = Image.FromFile(ofd.FileName);
                Bitmap bmp =new Bitmap(pic.Image) ;

                BitmapData bmpData = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, bmp.PixelFormat);


                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine();
                }
            }
        }

        private void grayScale_tsmi_Click(object sender, EventArgs e)
        {

                Bitmap pic_img = new Bitmap(pic.Image);
                Bitmap bmp = new Bitmap(pic.Image);

            unsafe
            {
                BitmapData bmpData = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixel = bmpData.Height;
                int widthInBytes = bmpData.Width * bytesPerPixel;

                byte* ptrFirstPixel = (byte*)bmpData.Scan0;
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine(ptrFirstPixel[i]);
                }
                return;
                System.Threading.Tasks.Parallel.For(0, heightInPixel, y =>
                {
                    byte* currentLine = ptrFirstPixel + (y * bmpData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];


                        int avg = (oldBlue + oldGreen + oldRed) / 3;
                        Color color = Color.FromArgb(1, avg, avg, avg);

                        currentLine[x] = color.B;
                        currentLine[x + 1] = color.G;
                        currentLine[x + 2] = color.R;
                    }
                });

                bmp.UnlockBits(bmpData);

                pic.Image = bmp;
            }

        }

        private void red_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Red;
        }

        private void green_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Green;
        }

        private void blue_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Blue;
        }

        private void cyan_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Cyan;
        }

        private void magenta_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Magenta;
        }

        private void yellow_tsmi_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Yellow;
        }
    }
}
