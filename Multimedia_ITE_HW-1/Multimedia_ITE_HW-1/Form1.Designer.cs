namespace Multimedia_ITE_HW_1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pic = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayScale_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.red_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.green_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.blue_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.cyan_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.magenta_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.yellow_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 24);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(1008, 663);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.colorModeToolStripMenuItem,
            this.colorsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.open_tsmi,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // open_tsmi
            // 
            this.open_tsmi.Name = "open_tsmi";
            this.open_tsmi.Size = new System.Drawing.Size(152, 22);
            this.open_tsmi.Text = "Open";
            this.open_tsmi.Click += new System.EventHandler(this.open_tsmi_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // colorModeToolStripMenuItem
            // 
            this.colorModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayScale_tsmi});
            this.colorModeToolStripMenuItem.Name = "colorModeToolStripMenuItem";
            this.colorModeToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.colorModeToolStripMenuItem.Text = "Color Mode";
            // 
            // grayScale_tsmi
            // 
            this.grayScale_tsmi.Name = "grayScale_tsmi";
            this.grayScale_tsmi.Size = new System.Drawing.Size(152, 22);
            this.grayScale_tsmi.Text = "GrayScale";
            this.grayScale_tsmi.Click += new System.EventHandler(this.grayScale_tsmi_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.red_tsmi,
            this.green_tsmi,
            this.blue_tsmi,
            this.cyan_tsmi,
            this.magenta_tsmi,
            this.yellow_tsmi});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.colorsToolStripMenuItem.Text = "Colors";
            // 
            // red_tsmi
            // 
            this.red_tsmi.Name = "red_tsmi";
            this.red_tsmi.Size = new System.Drawing.Size(152, 22);
            this.red_tsmi.Text = "Red";
            this.red_tsmi.Click += new System.EventHandler(this.red_tsmi_Click);
            // 
            // green_tsmi
            // 
            this.green_tsmi.Name = "green_tsmi";
            this.green_tsmi.Size = new System.Drawing.Size(152, 22);
            this.green_tsmi.Text = "Green";
            this.green_tsmi.Click += new System.EventHandler(this.green_tsmi_Click);
            // 
            // blue_tsmi
            // 
            this.blue_tsmi.Name = "blue_tsmi";
            this.blue_tsmi.Size = new System.Drawing.Size(152, 22);
            this.blue_tsmi.Text = "Blue";
            this.blue_tsmi.Click += new System.EventHandler(this.blue_tsmi_Click);
            // 
            // cyan_tsmi
            // 
            this.cyan_tsmi.Name = "cyan_tsmi";
            this.cyan_tsmi.Size = new System.Drawing.Size(152, 22);
            this.cyan_tsmi.Text = "Cyan";
            this.cyan_tsmi.Click += new System.EventHandler(this.cyan_tsmi_Click);
            // 
            // magenta_tsmi
            // 
            this.magenta_tsmi.Name = "magenta_tsmi";
            this.magenta_tsmi.Size = new System.Drawing.Size(152, 22);
            this.magenta_tsmi.Text = "Magenta";
            this.magenta_tsmi.Click += new System.EventHandler(this.magenta_tsmi_Click);
            // 
            // yellow_tsmi
            // 
            this.yellow_tsmi.Name = "yellow_tsmi";
            this.yellow_tsmi.Size = new System.Drawing.Size(152, 22);
            this.yellow_tsmi.Text = "Yellow";
            this.yellow_tsmi.Click += new System.EventHandler(this.yellow_tsmi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 687);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem open_tsmi;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayScale_tsmi;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem red_tsmi;
        private System.Windows.Forms.ToolStripMenuItem green_tsmi;
        private System.Windows.Forms.ToolStripMenuItem blue_tsmi;
        private System.Windows.Forms.ToolStripMenuItem cyan_tsmi;
        private System.Windows.Forms.ToolStripMenuItem magenta_tsmi;
        private System.Windows.Forms.ToolStripMenuItem yellow_tsmi;
    }
}

