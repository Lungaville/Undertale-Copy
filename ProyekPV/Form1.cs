using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekPV
{
    public partial class Form1 : Form
    {
        Map1 map1 = new Map1();
        Player player;
        Camera camera;
        public Bitmap map;
        Brush b = new SolidBrush(Color.Black);
       
        public Form1()
        {
            InitializeComponent();
            camera= new Camera(map1.startCameraX,map1.startCameraY);
            map = (Bitmap)Bitmap.FromFile("image/MapUndertale/Ruins/map1.png");
            player = new Player(map1.startPlayerX, map1.startPlayerY);
            #region
            /*   this.FormBorderStyle = FormBorderStyle.None;
            pictureBox1.Width = SystemInformation.VirtualScreen.Width;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            pictureBox1.Height = SystemInformation.VirtualScreen.Height;
            //crop posisi 8000 3000 sebesar screen width dan height
            // bp = ResizeImage((Bitmap)Bitmap.FromFile("map1.png"), bp.Width * 18 / 10 , bp.Height * 18/ 10);
            rect = new Rectangle(8000, 3000, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            */
            #endregion
        }
        #region
        /*
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        */
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) player.left = true;
            if (e.KeyCode == Keys.S)  player.down = true;
            if (e.KeyCode == Keys.D)player.right = true;
           if (e.KeyCode == Keys.W)player.up = true;
            /*
            Console.WriteLine("X" + player.posX);
            Console.WriteLine("Y" + player.posY);
            Console.WriteLine("Size Gambar Width " + map.Width);
            Console.WriteLine("Size Gambar Height " +map.Height);
            */
            movementTimer.Start();
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)player.left = false;

            if (e.KeyCode == Keys.S) player.down = false;
            
            if (e.KeyCode == Keys.D)  player.right = false;
            if (e.KeyCode == Keys.W) player.up = false;
            if (!player.left && !player.down && !player.right && !player.up) movementTimer.Stop();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(map, camera.camX, camera.camY,map.Width, map.Height);
            e.Graphics.DrawImage(Image.FromFile("image/char/"+player.sprites), player.posX, player.posY, 40, 40);
        }

        private void movementTimer_Tick(object sender, EventArgs e)
        {
            if (player.up) camera.camY += 5;
            if (player.down) camera.camY -= 5;
            if (player.left) camera.camX += 5;
            if (player.right) camera.camX -= 5;
        }

        private void FpsGame_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
    }
}