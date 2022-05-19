using ImageProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Temp.Models;

namespace Temp
{
    public partial class Form1 : Form
    {
        /* Defining Variables:
           * Image  >> to Store the Required Image.
           * Image2 >> output Image.
           * ImageData  >> to store The Image Data In the Memory (input image)
           * ImageData2 >> to store The Image Data In the Memory (output image)
           * buffer  >> buffering array used to edite the Image Data and to return back the edited ones to output array
           * buffer2 >> output array
           * grayscale >> to hold the grayscale value
           * r_x,g_x,b_x >> to hold the gradient in x components
           * r_y,g_y,b_y >> to hold the gradient in y components
           * r,g,b >> to hold the rgb values
           * weights_x >> x-Kernel
           * weights_y >> y-Kernel
           * pointer  >> to hold the address to the red value of the first pixel in the memory (input array)
           * pointer2 >> to hold the address to the red value of the first pixel in the memory (output array)
           * location >> to hold the location of current pixel in input image
           * location >> to hold the location of current pixel in the window
           * weight_x >> to hold the x-weight 
           * weight_y >> to hold the y-weight
           */
        private Bitmap Image, Image2;
        private BitmapData ImageData, ImageData2;
        private byte[] buffer, buffer2;
        private int b, g, r, r_x, g_x, b_x, r_y, g_y, b_y, grayscale, location, location2;


        Graphics graphics;

        int x = -1;
        int y = -1;

        bool isMove = false;

        Pen pen;

        Bitmap bmp;



        List<ColorMap> maps;




        private sbyte weight_x, weight_y;



        private sbyte[,] weights_x;
        private sbyte[,] weights_y;

        /*  private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
          {


              x0 = e.Location.X;
              y0 = e.Location.Y;


              Cursor = Cursors.WaitCursor;

              Bitmap image = new Bitmap(pictureBox1.Image);
              Color c0 = image.GetPixel(e.X, e.Y);
              Rectangle rect = new Rectangle(Point.Empty, image.Size);

              Color prev = image.GetPixel(e.X, e.Y);

              Stack<Point> stack = new Stack<Point>();
              List<Point> visited = new List<Point>();

              stack.Push(new Point(e.X, e.Y));
              Point up, down, right, left;
              while (stack.Any())
              {
                  Point pop = stack.Pop();

                  if (!rect.Contains(pop)) continue;

                  c0 = image.GetPixel(pop.X, pop.Y);

                  //Console.WriteLine("Point  :  " + pop.X + "   ,   " + pop.Y);
                  //Console.WriteLine("Point  :  " + pop.X + "   ,   " + pop.Y);

                  if (prev.GetBrightness() == c0.GetBrightness() && !visited.Contains(pop))
                  {

                      visited.Add(pop);

                      image.SetPixel(pop.X, pop.Y, Color.Yellow);

                      up = new Point(pop.X, pop.Y + 1);
                      if (!visited.Contains(up))
                          stack.Push(up);

                      down = new Point(pop.X, pop.Y - 1);
                      if (!visited.Contains(down))
                          stack.Push(down);

                      right = new Point(pop.X + 1, pop.Y);
                      if (!visited.Contains(right))
                          stack.Push(right);

                        left = new Point(pop.X - 1, pop.Y);
                      if (!visited.Contains(left))
                          stack.Push(left);
                  }

                  prev = c0;
              }


              pictureBox1.Image = image;
              Cursor = Cursors.Default;

          }*/


        Color[] colors;
        private List<CustomPoint> points = new List<CustomPoint>();

        private void recolor_diagram_Click(object sender, EventArgs e)
        {
            //colors = ColorBuilder.GetColorDiagram(points);
            //Bitmap colorBitmap = new Bitmap(Image);
            //for (int i = 0; i < Image.Width; i++)
            //{
            //    for (int j = 0; j < Image.Height; j++)
            //    {
            //        Color c = Image.GetPixel(i, j);
            //        double gray = c.R * 0.11 + c.G * 0.59 + c.B * 0.3;
            //        colorBitmap.SetPixel(i, j, colors[(int)gray]);
            //    }
            //}
            //pictureBox1.Image = colorBitmap;

            /* pictureBox1.Image = Pseudocolorize(Image);*/
        }

        Bitmap Pseudocolorize(Bitmap image)
        {
            int w = image.Width;
            int h = image.Height;

            BitmapData image_data = image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly,
                image.PixelFormat);

            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);

            byte[] unique = buffer.Distinct().ToArray();
            Array.Sort(unique);

            int blue = 255;
            int green = 0;
            int red = 0;

            byte[][] color_scale = new byte[unique.Length][];

            for (int i = 0; i < unique.Length; i++)
            {
                color_scale[i] = new byte[3];
                byte step = (byte)Math.Round(512f / unique.Length);

                if (blue > 0 && green < 255)
                {
                    color_scale[i][0] = (byte)blue;
                    color_scale[i][1] = (byte)green;
                    color_scale[i][2] = 0;

                    blue -= step;
                    green += step;

                    if (blue < 0)
                    {
                        blue = 0;
                    }

                    if (green > 255)
                    {
                        green = 255;
                    }
                }

                else
                {
                    color_scale[i][0] = 0;
                    color_scale[i][1] = (byte)green;
                    color_scale[i][2] = (byte)red;

                    green -= step;
                    red += step;

                    if (green < 0)
                    {
                        green = 0;
                    }

                    if (red > 255)
                    {
                        red = 255;
                    }
                }
            }

            byte[] result = new byte[bytes];
            for (int i = 0; i < bytes; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i + j] = color_scale[Array.IndexOf(unique, buffer[i + j])][j];
                }
            }
            Bitmap result_image = new Bitmap(w, h);
            BitmapData result_data = result_image.LockBits(
                new Rectangle(0, 0, w, h),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(result, 0, result_data.Scan0, bytes);
            result_image.UnlockBits(result_data);
            return result_image;
        }

        private void btn_red_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Red;

        }

        private void btn_green_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Green;
        }

        private void btn_blue_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Blue;
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Color c = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);
            //  Console.WriteLine(c.GetBrightness());

            Point sPt = scaledPoint(pictureBox1, e.Location);
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
            Fill4(bmp, sPt, c0, pen.Color);
            pictureBox1.Image = bmp;

            //Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            //for (int i = 0; i < 100; i++)
            //{
            //    Color c = bmp.GetPixel(i, e.Y);
            //    Console.WriteLine(
            //        "Red : {0} ,Green : {1} ,Blue : {2} ,Brightness : {3}",
            //        c.R,c.G,c.B,c.GetBrightness()
            //        );

            //    //bmp.SetPixel(i, e.Y, Color.FromArgb(255, 0, 0));
            //    //bmp.SetPixel(i, e.Y+1, Color.FromArgb(255, 0, 0));
            //    //bmp.SetPixel(i, e.Y+2, Color.FromArgb(255, 0, 0));
            //    //bmp.SetPixel(i, e.Y+3, Color.FromArgb(255, 0, 0));
            //    //bmp.SetPixel(i, e.Y+4, Color.FromArgb(255, 0, 0));
            //    //bmp.SetPixel(i, e.Y+5, Color.FromArgb(255, 0, 0));

            //}



        }
        System.Drawing.Point po = new System.Drawing.Point();
        string imgPath = "";

        void Reload()
        {
            pictureBox1.Image = new Bitmap(Image);
        }

        int count = 0;



        protected override void OnMouseWheel(MouseEventArgs e)
        {

            //int x = e.Location.X;
            //int y = e.Location.Y;

            //int ow = pictureBox1.Width;
            //int oh = pictureBox1.Height;

            //int VX, VY;

            //if (e.Delta > 0)
            //{
            //    pictureBox1.Width += 60;
            //    pictureBox1.Height += 60;

            //    PropertyInfo pinfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            //    Rectangle rect = (Rectangle)pinfo.GetValue(pictureBox1, null);
            //    pictureBox1.Width = rect.Width;
            //    pictureBox1.Height = rect.Height;
            //}
            //if (e.Delta < 0)
            //{
            //    pictureBox1.Width -= 60;
            //    pictureBox1.Height -= 60;

            //    PropertyInfo pinfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            //    Rectangle rect = (Rectangle)pinfo.GetValue(pictureBox1, null);
            //    pictureBox1.Width = rect.Width;
            //    pictureBox1.Height = rect.Height;
            //}

            //VX = (int)((double)x * (ow - pictureBox1.Width) / ow);
            //VY = (int)((double)y * (oh - pictureBox1.Height) / oh);


            //pictureBox1.Location = new Point(pictureBox1.Location.X + VX, pictureBox1.Location.Y + VY);





            ////  flag = 1;
            //// Override OnMouseWheel event, for zooming in/out with the scroll wheel
            //if (pictureBox1.Image != null)
            //{
            //    // If the mouse wheel is moved forward (Zoom in)
            //    if (e.Delta > 0)
            //    {
            //        // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
            //        if ((pictureBox1.Width < (15 * this.Width)) && (pictureBox1.Height < (15 * this.Height)))
            //        {
            //            // Change the size of the picturebox, multiply it by the ZOOMFACTOR
            //            pictureBox1.Width = (int)(pictureBox1.Width * 1.25);
            //            pictureBox1.Height = (int)(pictureBox1.Height * 1.25);

            //            // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
            //            pictureBox1.Top = (int)(e.Y - 1.25 * (e.Y - pictureBox1.Top));
            //            pictureBox1.Left = (int)(e.X - 1.25 * (e.X - pictureBox1.Left));
            //        }
            //    }
            //    else
            //    {
            //        // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
            //        if ((pictureBox1.Width > (Image.Width)) && (pictureBox1.Height > (Image.Height)))
            //        {
            //            // Change the size of the picturebox, divide it by the ZOOMFACTOR
            //            pictureBox1.Width = (int)(pictureBox1.Width / 1.25);
            //            pictureBox1.Height = (int)(pictureBox1.Height / 1.25);

            //            // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
            //            pictureBox1.Top = (int)(e.Y - 0.80 * (e.Y - pictureBox1.Top));
            //            pictureBox1.Left = (int)(e.X - 0.80 * (e.X - pictureBox1.Left));
            //        }
            //    }
            //}





            if (pictureBox1.Image != null)
            {

                //Console.WriteLine(e.Delta);

                po.X = pictureBox1.Image.Width / 2;
                po.Y = pictureBox1.Image.Height / 2;

                int x = 0; int y = 0;
                Reload();
                //x += e.Delta / 60;
                //y += e.Delta / 60;



                count += e.Delta / 60;

                Image myimg = pictureBox1.Image;
                Bitmap bmp = new Bitmap(myimg.Width, myimg.Height);

                ImageAttributes ia = new ImageAttributes();

                Graphics g = Graphics.FromImage(bmp);
                //g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);

                g.TranslateTransform(e.X, e.Y);
                if (count > 0 || count > 0)
                    g.ScaleTransform(count, count);
                g.TranslateTransform(-e.X, -e.Y);

                //pictureBox1.Top = (int)(e.Y - po.Y);
                //pictureBox1.Left = (int)(e.X - po.X);


                // g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(myimg, new Rectangle(0, 0, myimg.Width, myimg.Height), 0, 0, myimg.Width, myimg.Height, GraphicsUnit.Pixel, ia);


                //pictureBox1.Top = (int)(e.Y - 1.25 * (e.Y - pictureBox1.Top));
                //pictureBox1.Left = (int)(e.X - 1.25 * (e.X - pictureBox1.Left));


                g.Dispose();
                pictureBox1.Image = bmp;
            }

            //if (count >= 0 && count < 100)
            //{
            //    count += e.Delta / 60;
            //    pictureBox1.Image = ZoomImage(Image, new Size(count, count));


            //}



        }
        static void Fill4(Bitmap bmp, Point pt, Color c0, Color c1)
        {
            Color cx = bmp.GetPixel(pt.X, pt.Y);
            if (cx.GetBrightness() < 0.01f) return;  // optional, to prevent filling a black grid
            Rectangle bmpRect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;

            stack.Push(new Point(x0, y0));
            while (stack.Any())
            {
                Point p = stack.Pop();
                if (!bmpRect.Contains(p)) continue;
                cx = bmp.GetPixel(p.X, p.Y);
                if (

                    cx.R == cx.G && cx.G == cx.B &&
                    cx.GetBrightness() - 0.02 <= c0.GetBrightness() &&
                    c0.GetBrightness() <= cx.GetBrightness() + 0.02)  //*
                {
                    bmp.SetPixel(p.X, p.Y, c1);

                    stack.Push(new Point(p.X, p.Y + 1));
                    stack.Push(new Point(p.X, p.Y - 1));
                    stack.Push(new Point(p.X + 1, p.Y));
                    stack.Push(new Point(p.X - 1, p.Y));
                }
            }
        }

        Image ZoomImage(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.Bicubic;
           // g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
            return bmp;
        }

        //private void trackBar1_Scroll(object sender, EventArgs e)
        //{
        //    if (trackBar1.Value > 0)
        //    {
                
        //        pictureBox1.Image = ZoomImage(Image, new Size(trackBar1.Value, trackBar1.Value));
        //    }
        //}


        static Point scaledPoint(PictureBox pb, Point pt)
        {
            float scaleX = 1f * pb.Image.Width / pb.ClientSize.Width;
            float scaleY = 1f * pb.Image.Height / pb.ClientSize.Height;
            return new Point((int)(pt.X * scaleX), (int)(pt.Y * scaleY));
        }

        private IntPtr pointer, pointer2;
        public Form1()
        {
            InitializeComponent();
            weights_x = new sbyte[,] { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
            weights_y = new sbyte[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };




            graphics = pictureBox1.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            pen = new Pen(Color.Yellow, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            maps = new List<ColorMap>();
        }
        /* Saving the Image file:
         * type the file name followed by the file extension (for example new.jpg)
         */
        private void savebtn_Click(object sender, EventArgs e)
        {








            //Color c = Color.Brown;
            //Color c2 = Color.Fuchsia;

            //Console.WriteLine(c.R +"  :  "+c2.R);

            //byte b1 = (byte)(c.R * c2.R >> 8);
            //Console.WriteLine(b1);



            //SaveFileDialog sfd = new SaveFileDialog();
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBox1.Image.Save(sfd.FileName, ImageFormat.Bmp);

            //}
        }
        /* Loading the Image file
         * Showing it in the picturebox
         */
        private void loadbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image = new Bitmap(ofd.FileName);
                imgPath = ofd.FileName;

                pictureBox1.Width = Image.Width;
                pictureBox1.Height = Image.Height;
                Image2 = new Bitmap(Image.Width, Image.Height);
            }
            pictureBox1.Image = Image;


            //List<Point> points =new List<Point>
            //{
            //    new Point(10,5),new Point(11,5),new Point(10,6),
            //    new Point(17,5),new Point(10,15),new Point(8,5),
            //    new Point(10,5),new Point(16,5),new Point(10,25),
            //};

            //if (points.Contains(new Point(10, 35)))
            //{
            //    MessageBox.Show("Contains");
            //}
            //else
            //{
            //    MessageBox.Show("Not Contains");
            //}

        }
        /* Converting The Image file:
         * 1-Lock the Image Bits in the memory (PixelFormat.Format24bppRgb means that the program is going to lock only red , green and blue without including the alpha channel)
         * 2-initializing the buffer array it's going to have all the image data (the image have height and width which leads to total pixel count = height * width and each pixel have r,g,b so the array length = height*width*3)
         * 3-set the pointer to the location of the red value of the first pixel of the image
         * 4-copy the Image Data to the Buffer Array
         * 5-Loop through each pixel and make the loop step = 3 (i+=3)
         * 6-apply the window on the current pixel
         * 7-multiply each pixel in the window to each corresponding weight
         * 8-assign the channels total values to output array once the you finished looping through the window
         * 9-unlock the image bits
         */
        private void convertbtn_Click(object sender, EventArgs e)
        {
            ImageData = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            ImageData2 = Image2.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            buffer = new byte[ImageData.Stride * Image.Height];
            buffer2 = new byte[ImageData.Stride * Image.Height];
            pointer = ImageData.Scan0;
            pointer2 = ImageData2.Scan0;
            Marshal.Copy(pointer, buffer, 0, buffer.Length);
            for (int y = 0; y < Image.Height; y++)
            {
                for (int x = 0; x < Image.Width * 3; x += 3)
                {
                    r_x = g_x = b_x = 0; //reset the gradients in x-direcion values
                    r_y = g_y = b_y = 0; //reset the gradients in y-direction values
                    location = x + y * ImageData.Stride; //to get the location of any pixel >> location = x + y * Stride
                    for (int yy = -(int)Math.Floor(weights_y.GetLength(0) / 2.0d), yyy = 0; yy <= (int)Math.Floor(weights_y.GetLength(0) / 2.0d); yy++, yyy++)
                    {
                        if (y + yy >= 0 && y + yy < Image.Height) //to prevent crossing the bounds of the array
                        {
                            for (int xx = -(int)Math.Floor(weights_x.GetLength(1) / 2.0d) * 3, xxx = 0; xx <= (int)Math.Floor(weights_x.GetLength(1) / 2.0d) * 3; xx += 3, xxx++)
                            {
                                if (x + xx >= 0 && x + xx <= Image.Width * 3 - 3) //to prevent crossing the bounds of the array
                                {
                                    location2 = x + xx + (yy + y) * ImageData.Stride; //to get the location of any pixel >> location = x + y * Stride
                                    weight_x = weights_x[yyy, xxx];
                                    weight_y = weights_y[yyy, xxx];
                                    //applying the same weight to all channels
                                    b_x += buffer[location2] * weight_x;
                                    g_x += buffer[location2 + 1] * weight_x; //G_X
                                    r_x += buffer[location2 + 2] * weight_x;
                                    b_y += buffer[location2] * weight_y;
                                    g_y += buffer[location2 + 1] * weight_y;//G_Y
                                    r_y += buffer[location2 + 2] * weight_y;
                                }
                            }
                        }
                    }
                    //getting the magnitude for each channel
                    b = (int)Math.Sqrt(Math.Pow(b_x, 2) + Math.Pow(b_y, 2));
                    g = (int)Math.Sqrt(Math.Pow(g_x, 2) + Math.Pow(g_y, 2));//G
                    r = (int)Math.Sqrt(Math.Pow(r_x, 2) + Math.Pow(r_y, 2));

                    if (b > 255) b = 255;
                    if (g > 255) g = 255;
                    if (r > 255) r = 255;

                    //getting grayscale value
                    grayscale = (b + g + r) / 3;

                    //thresholding to clean up the background
                    //if (grayscale < 80) grayscale = 0;
                    buffer2[location] = (byte)grayscale;
                    buffer2[location + 1] = (byte)grayscale;
                    buffer2[location + 2] = (byte)grayscale;
                    //thresholding to clean up the background
                    //if (b < 100) b = 0;
                    //if (g < 100) g = 0;
                    //if (r < 100) r = 0;

                    //buffer2[location] = (byte)b;
                    //buffer2[location + 1] = (byte)g;
                    //buffer2[location + 2] = (byte)r;
                }
            }
            Marshal.Copy(buffer2, 0, pointer2, buffer.Length);
            Image.UnlockBits(ImageData);
            Image2.UnlockBits(ImageData2);
            pictureBox1.Image = Image2;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            Color backColor = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);

            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = backColor;
            colorMap.NewColor = pen.Color;
            maps.Add(colorMap);

            int level = Image.GetPixel(e.X, e.Y).B;
            // points.Add(new CustomPoint(e.Location, pen.Color));

            //foreach (ColorMap color in maps)
            //{
            //    Console.WriteLine(color.OldColor.GetBrightness());

            //}


            Image = new Bitmap(pictureBox1.Image);

            Bitmap bmp = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < points.Count; i++)
            {
                Point pt = points[i].Point;
                //Color c0 = bmp.GetPixel(pt.X, pt.Y);

                //Fill4(bmp, pt, c0, pen.Color);


                Point sPt = scaledPoint(pictureBox1, points[i].Point);
                // Bitmap bmp = (Bitmap)pictureBox1.Image;
                Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
                Fill4(bmp, sPt, c0, points[i].Color);
                // pictureBox1.Image = bmp;

            }

            points.Clear();

            pictureBox1.Image = bmp;


            isMove = false;
            x = -1;
            y = -1;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {

                Color backColor = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);

                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = backColor;
                colorMap.NewColor = pen.Color;
                maps.Add(colorMap);


                int level = Image.GetPixel(e.X, e.Y).B;
                points.Add(new CustomPoint(e.Location, pen.Color));

                graphics.DrawLine(pen, new Point(x, y), e.Location);


               

                x = e.X;
                y = e.Y;
            }

            //this.Text = "X :  " + e.X + "  Y : " + e.Y;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            //Color backColor = ((Bitmap)pictureBox1.Image).GetPixel(e.X, e.Y);

            //ColorMap colorMap = new ColorMap();
            //colorMap.OldColor = backColor;
            //colorMap.NewColor = pen.Color;
            //maps.Add(colorMap);


            //  int level = Image.GetPixel(e.X, e.Y).B;
            // points.Add(new CustomPoint(e.Location, pen.Color));


            //x0 = e.Location.X;
            //y0 = e.Location.Y;
          

            x = e.X;
            y = e.Y;

            isMove = true;
        }

        private void recolor_Click(object sender, EventArgs e)


        {
            Bitmap pic_img = new Bitmap(pictureBox1.Image);
            Bitmap bmp = new Bitmap(pictureBox1.Image);


            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int heightInPixel = bmpData.Height;
            int widthInBytes = bmpData.Width * bytesPerPixel;


            int byteCount = bmpData.Stride * pic_img.Height;
            byte[] pixels = new byte[byteCount];

            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;


            //Console.WriteLine(maps.Count);
            maps = maps.Distinct(new CustomComparer()).ToList();
            //Console.WriteLine(maps.Count);

            Parallel.For(0, heightInPixel, y =>
            {

                int currentLine = y * bmpData.Stride;


                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {


                    //Console.WriteLine(
                    //  "Blue : " + pixels[currentLine + x] +
                    //  " , Green : " + pixels[currentLine + x + 1] +
                    //  " , Red : " + pixels[currentLine + x + 2] +
                    //   " , Brightness : " + pixels[currentLine + x + 3]);


                    int oldBlue = pixels[currentLine + x];
                    int oldGreen = pixels[currentLine + x + 1];
                    int oldRed = pixels[currentLine + x + 2];



                    Color color = Color.FromArgb(oldRed, oldGreen, oldBlue);
                    //                  Console.WriteLine(
                    //                    "Blue : " + c.B +
                    //                    " , Green : " + c.G +
                    //                    " , Red : " + c.R +
                    //                     " , Brightness : " + c.GetBrightness()
                    //                     );
                    //Console.WriteLine();

                    foreach (ColorMap colorMap in maps)
                    {

                        if (
                        colorMap.OldColor.B == color.B &&
                        colorMap.OldColor.G == color.G &&
                        colorMap.OldColor.R == color.R
                        &&
                        color.GetBrightness() - 0.08 <= colorMap.OldColor.GetBrightness() &&
                        colorMap.OldColor.GetBrightness() <= color.GetBrightness() + 0.08
                        )
                        {

                            /***
                            oldBlue =(int)( (colorMap.NewColor.B * 0.8) + (oldBlue * 0.2));
                            oldGreen = (int)((colorMap.NewColor.G * 0.8) + (oldGreen * 0.2));
                            oldRed = (int)((colorMap.NewColor.R * 0.8 )+ (oldRed * 0.2));
                            ***/

                            oldBlue = colorMap.NewColor.B;
                            oldGreen = colorMap.NewColor.G;
                            oldRed = colorMap.NewColor.R;
                        }
                    }


                    pixels[currentLine + x] = (byte)oldBlue;
                    pixels[currentLine + x + 1] = (byte)oldGreen;
                    pixels[currentLine + x + 2] = (byte)oldRed;
                }
            });

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);

            bmp.UnlockBits(bmpData);

            pictureBox1.Image = bmp;

        }

        private void gray_Click(object sender, EventArgs e)
        {
            Bitmap pic_img = new Bitmap(pictureBox1.Image);
            Bitmap bmp = new Bitmap(pictureBox1.Image);


            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int heightInPixel = bmpData.Height;
            int widthInBytes = bmpData.Width * bytesPerPixel;


            int byteCount = bmpData.Stride * pic_img.Height;
            byte[] pixels = new byte[byteCount];

            IntPtr ptrFirstPixel = bmpData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;


            Parallel.For(0, heightInPixel, y =>
            {

                int currentLine = y * bmpData.Stride;


                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {

                    int oldBlue = pixels[currentLine + x];
                    int oldGreen = pixels[currentLine + x + 1];
                    int oldRed = pixels[currentLine + x + 2];


                    int avg = (oldBlue + oldGreen + oldRed) / 3;
                    Color color = Color.FromArgb(1, avg, avg, avg);


                    pixels[currentLine + x] = (byte)avg;
                    pixels[currentLine + x + 1] = (byte)avg;
                    pixels[currentLine + x + 2] = (byte)avg;
                }
            });

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);

            bmp.UnlockBits(bmpData);

            pictureBox1.Image = bmp;

        }

        int x0; int y0;



       
        private void recolor2_Click(object sender, EventArgs e)
        {


            Bitmap bmp = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < points.Count; i++)
            {
                Point pt = points[i].Point;
                //Color c0 = bmp.GetPixel(pt.X, pt.Y);

                //Fill4(bmp, pt, c0, pen.Color);


                Point sPt = scaledPoint(pictureBox1, points[i].Point);
                // Bitmap bmp = (Bitmap)pictureBox1.Image;
                Color c0 = bmp.GetPixel(sPt.X, sPt.Y);
                Fill4(bmp, sPt, c0, points[i].Color);
                // pictureBox1.Image = bmp;

            }

            pictureBox1.Image = bmp;

        }

        static void Fill(Bitmap bmp, Point pt, Color c0, Color c1)
        {


            Color cx = bmp.GetPixel(pt.X, pt.Y);


            if (cx.GetBrightness() < 0.01f)
                return;

            Rectangle rect = new Rectangle(Point.Empty, bmp.Size);
            Stack<Point> stack = new Stack<Point>();
            int x0 = pt.X;
            int y0 = pt.Y;

            stack.Push(new Point(x0, y0));
            Color pc = c1;

            while (stack.Any())
            {

                Point p = stack.Pop();

                if (!rect.Contains(p)) continue;

                cx = bmp.GetPixel(p.X, p.Y);



                if (cx.GetBrightness() == c0.GetBrightness())
                {
                    bmp.SetPixel(p.X, p.Y, c1);
                    bmp.SetPixel(p.X + 1, p.Y, c1);
                    bmp.SetPixel(p.X + 2, p.Y, c1);
                    bmp.SetPixel(p.X + 3, p.Y, c1);
                    bmp.SetPixel(p.X + 4, p.Y, c1);


                    stack.Push(new Point(p.X, p.Y + 1));

                    //stack.Push(new Point(p.X, p.Y - 1));
                    //stack.Push(new Point(p.X + 1, p.Y));
                    //stack.Push(new Point(p.X - 1, p.Y));
                }


            }







            //Color cx = bmp.GetPixel(pt.X, pt.Y);


            //if (cx.GetBrightness() < 0.01f)
            //    return;

            //Rectangle rect = new Rectangle(Point.Empty, bmp.Size);
            //Stack<Point> stack = new Stack<Point>();
            //int x0 = pt.X;
            //int y0 = pt.Y;

            //stack.Push(new Point(x0, y0));
            //Color pc = c1;

            //while (stack.Any())
            //{

            //    Point p = stack.Pop();

            //    if (!rect.Contains(p)) continue;

            //    cx = bmp.GetPixel(p.X, p.Y);



            //    if (cx.GetBrightness() == c0.GetBrightness())
            //    {
            //        bmp.SetPixel(p.X, p.Y, c1);

            //        stack.Push(new Point(p.X, p.Y + 1));
            //        stack.Push(new Point(p.X, p.Y - 1));
            //        stack.Push(new Point(p.X + 1, p.Y));
            //        stack.Push(new Point(p.X - 1, p.Y));
            //    }


            //}
        }



        //static Point scaledPoint(PictureBox pic, Point p)
        //{
        //    int scaleX = 1 * pic.Width / pic.ClientSize.Width;
        //    int scaley = 1 * pic.Height / pic.ClientSize.Height;

        //    return new Point(p.X * scaleX, p.Y * scaley);
        //}

    }

}
