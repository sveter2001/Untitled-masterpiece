using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Untitled_masterpiece
{
    public partial class Untitled_masterpiece : Form
    {
        Image playerImg;
        Image grassImg;
        //private int currFrame = 0;
        private int currAnimation = 0;
        private bool isPressedAnyKey = false;
        Player player;

        int width = 10;
        int height = 10;

        int[,] map;
        int sideOfMapObject;
        Point delta;

        public Untitled_masterpiece()
        {
            InitializeComponent();
            // Используйте двойную буферизацию для уменьшения мерцания.
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            playerImg = new Bitmap("D:\\slime.png");
            grassImg = new Bitmap("D:\\grass.png");
            player = new Player(new Size(128,128),100,100,playerImg);
            timer2.Interval = 10;
            timer2.Tick += new EventHandler(updateMove);
            timer2.Start();

            timer1.Interval = 75;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            sideOfMapObject = 128;

            this.KeyDown += new KeyEventHandler(keyboard);
            this.KeyUp += new KeyEventHandler(freeKeyb);

            delta = new Point(0,0);

            map = new int[10, 10]{
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, } ;

        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            CreateMap(gr);
            PlayAnimation(gr);

        }
        private void CreateMap(Graphics gr)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] == 0)
                    {
                        gr.DrawImage(grassImg, j * 128+delta.X, i * 128 + delta.Y, new Rectangle(new Point(0, 0), new Size(128, 128)), GraphicsUnit.Pixel);
                    }
                    else if (map[i, j] == 9)
                    {
                        //gr.DrawImage(dirtImg, j * 80 + delta.X, i * 80 + delta.Y, new Rectangle(new Point(0, 0), new Size(80, 80)), GraphicsUnit.Pixel);
                    }
                }
            }
        }


        private void updateMove(object sender, EventArgs e)
        {
            switch (currAnimation)
            {
                case 1:
                    //currAnimation = 1;
                    player.Left();
                    if (player._x > this.Width / 2 && player._x < sideOfMapObject * width - this.Width / 2)
                    delta.X+=player.speed;
                    //pictureBox1.Location = new Point(pictureBox1.Location.X - 2, pictureBox1.Location.Y);
                    break;
                case 2:
                    //currAnimation = 2;
                    player.Right();
                    if (player._x > this.Width / 2 && player._x < sideOfMapObject * width - this.Width / 2)
                        delta.X -= player.speed;
                    //pictureBox1.Location = new Point(pictureBox1.Location.X + 2, pictureBox1.Location.Y);
                    break;

            }
        }

        private void freeKeyb(object sender, KeyEventArgs e)
        {
            isPressedAnyKey = false;
            if (currAnimation == 1)
            {
                player.currFrame = 1;
                currAnimation = 5;
            }
                
            if (currAnimation == 2)
            {
                player.currFrame = 1;
                currAnimation = 6;
            }
               
        }

        private void keyboard(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    currAnimation = 1;
                    break;
                case "D":
                    currAnimation = 2;
                    break;
                case "w":
                    currAnimation = 3;
                    break;
                case "s":
                    currAnimation = 4;
                    break;

            }
            if (e.KeyCode.ToString() == "A" || e.KeyCode.ToString() == "D")
            isPressedAnyKey = true;
        }

        private void update(object sender, EventArgs e)
        {
            /*if (isPressedAnyKey)
            {
                playAnimationMove();
            }
            else
            {
                playAnimationIdle();
            }

            if (currFrame == 9)
                currFrame = 0;
            currFrame++;*/

            //gr.Clear(Color.White);

            if (isPressedAnyKey)
            {
                timer1.Interval = 75;
                //playAnimationMove();
                if (player.currFrame == 9)
                    player.currFrame = 0;
            }
            else
            {
                timer1.Interval = 75;
                //playAnimationIdle();
                if (player.currFrame == 9)
                    player.currFrame = 0;
            }

            player.currFrame++;
            Invalidate();



        }

        private void PlayAnimation(Graphics gr)
        {
            if (isPressedAnyKey)
            {
                if (currAnimation == 1)
                {
                    /*Image part = new Bitmap(128, 128);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(playerImg, 0, 0, new Rectangle(new Point(128 * currFrame, 128 * 2), new Size(128, 128)), GraphicsUnit.Pixel);
                    pictureBox1.Size = new Size(128, 128);
                    pictureBox1.Image = part;*/
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X, player._y + delta.Y, new Rectangle(new Point(128 * player.currFrame, 128 * 1 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
                if (currAnimation == 2)
                {
                    /*Image part = new Bitmap(128, 128);
                    Graphics g = Graphics.FromImage(part);
                    g.DrawImage(playerImg, 0, 0, new Rectangle(new Point(1280 - 128* currFrame, 128 * 2), new Size(128, 128)), GraphicsUnit.Pixel);
                    pictureBox1.Size = new Size(128, 128);
                    pictureBox1.Image = part;*/
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X, player._y + delta.Y, new Rectangle(new Point(2560 - 128 * player.currFrame, 128 * 1 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
            }
            else
            {
                    if (currAnimation == 5)
                    {
                        /*Image part = new Bitmap(128, 128);
                        Graphics g = Graphics.FromImage(part);
                        g.DrawImage(playerImg, 0, 0, new Rectangle(new Point(128 * currFrame, 128 * 0), new Size(128, 128)), GraphicsUnit.Pixel);
                        pictureBox1.Size = new Size(128, 128);
                        pictureBox1.Image = part;*/
                        gr.DrawImage(player._spritesAnimation, player._x + delta.X, player._y + delta.Y, new Rectangle(new Point(128 * player.currFrame, 128 * 0 /*(player.currAnimation - 5)*/), new Size(128, 128)), GraphicsUnit.Pixel);
                    }
                    if (currAnimation == 6)
                    {
                        /*Image part = new Bitmap(128, 128);
                        Graphics g = Graphics.FromImage(part);
                        g.DrawImage(playerImg, 0, 0, new Rectangle(new Point(1280 - 128 * currFrame, 128 * 0), new Size(128, 128)), GraphicsUnit.Pixel);
                        pictureBox1.Size = new Size(128, 128);
                        pictureBox1.Image = part;*/
                        gr.DrawImage(player._spritesAnimation, player._x + delta.X, player._y + delta.Y, new Rectangle(new Point(2560 - 128 * player.currFrame, 128 * 0 /*(player.currAnimation - 5)*/), new Size(128, 128)), GraphicsUnit.Pixel);
                    }
            }

        }








        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void button_Start_Click(object sender, EventArgs e)
        {
            button_Start.Enabled = false;
            button_Start.Visible = false;
            button_Choose_level.Enabled = false;
            button_Choose_level.Visible = false;
            button_Exit.Enabled = false;
            button_Exit.Visible = false;






        }
       
    }
}
