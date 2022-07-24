using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Untitled_masterpiece
{
    public partial class Untitled_masterpiece : Form
    {
        Image playerImg;
        Image grassImg;
        //private int currFrame = 0;
        private int currAnimation = 6;
        private bool isPressedAnyKey = false;
        Player player;
        //PlayerMod playermod;
        Vector jumpL = new Vector(-6, -35);
        Vector jumpR = new Vector(6, -35);
        Vector vector;
        Vector check = new Vector(0,0);
        int width = 20;
        int height = 10;
        bool camera = false;

        int[,] map;
        int sideOfMapObject;
        Point delta;

        Vector dir;

        public Untitled_masterpiece()
        {
            InitializeComponent();
            // Используйте двойную буферизацию для уменьшения мерцания.
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);


            playerImg = new Bitmap("D:\\slime.png");
            grassImg = new Bitmap("D:\\grass.png");

            dir = new Vector(0, 0);

            player = new Player(new Size(128,128),272,100,playerImg,dir);
            timer2.Interval = 1;
            timer2.Tick += new EventHandler(updateMove);
            timer2.Start();

            timer1.Interval = 50;
            timer1.Tick += new EventHandler(update);
            timer1.Start();


            sideOfMapObject = 128;

            this.KeyDown += new KeyEventHandler(keyboard);
            this.KeyUp += new KeyEventHandler(freeKeyb);

            delta = new Point(0,0);


            map = new int[10, 20]{
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } } ;

        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            CreateMap(gr);
            PlayAnimation(gr);
        }

        private void CreateMap(Graphics gr)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width ; j++)
                {
                    if (map[i, j] == 0)
                    {
                        gr.DrawImage(grassImg, j * 128+delta.X, i * 128 + delta.Y, new Rectangle(new Point(0, 0), new Size(128, 128)), GraphicsUnit.Pixel);
                    }
                    else if (map[i, j] == 1)
                    {
                        MakePlatform(j,i,gr);
                        //gr.DrawImage(dirtImg, j * 80 + delta.X, i * 80 + delta.Y, new Rectangle(new Point(0, 0), new Size(80, 80)), GraphicsUnit.Pixel);
                    }
                }
            }
        }

        private void MakePlatform(int i, int j, Graphics gr)
        {
            Point platfom = new Point(i*128 + delta.X, j*128 + delta.Y);
            Point platformEnd = new Point(platfom.X + 128, platfom.Y);
            Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);
            gr.DrawLine(skyBluePen, platfom, platformEnd);
            skyBluePen.Dispose();

        }

        private void updateMove(object sender, EventArgs e)
        {

            


            switch (currAnimation)
            {
                case 1:
                    player.Left(); 
                    break;
                case 2:
                    player.Right(); 
                    break;
                case 3:
                    player.Jump(jumpL);
                    player._dir.Y += 1;//гравитация при прыжке
                    break;
                case 4:
                    player.Jump(jumpR);
                    player._dir.Y += 1;//гравитация при прыжке
                    break;

            }
            
            System.Windows.Point startingpoint = new System.Windows.Point(player._x, player._y);
            Vector dir = new Vector(player._dir.X, player._dir.Y);
            System.Windows.Point pointResult = new System.Windows.Point();

            pointResult = startingpoint + dir;

            player._x = (int)pointResult.X;
            player._y = (int)pointResult.Y;

            if (player._dir.Y <= 5)//гравитация
            {
                player._dir.Y += 15;
            }

            //if ((player._y > this.Height / 3) && (player._y < this.Height / 3 * 2))
            //{
            //    camera = true;
            //}
            //else
            //{
            //    if (player._y < this.Height / 3 * 2 && (player._y < sideOfMapObject * height - this.Height / 2) && !camera)
            //        delta.Y = delta.Y + 1;
            //    else
            //    {
            //        delta.Y = delta.Y;
            //    }
            //    if (player._y > this.Height / 3 && (player._y < sideOfMapObject * height - this.Height / 2) && !camera)
            //        delta.Y = delta.Y - 1;
            //    else
            //    {
            //        delta.Y = delta.Y;
            //    }
            //}
            //if (((player._y > this.Height / 3) && (player._y < sideOfMapObject * height - this.Height / 3)) && !camera)
            //    delta.Y += -player.speed;
            //if (((player._y < this.Height / 3 * 2) && (player._y < sideOfMapObject * height - this.Height / 3)) && !camera)
            //    delta.Y += +player.speed;
            //delta.Y -= player.speed; delta.Y -= (int)player._dir.Y;

            //delta.Y = this.Height/2 - player._y;    //delta.Y = this.Height/2 - player._y; такой камерой можно сделать эффект землетрясения

            camera = false;
            vector.X = 0;
            vector.Y = player._dir.Y;
            if (map[Math.Abs(player._y) / 128, Math.Abs(player._x) / 128] == 1)
            {
                player._dir += -1 * vector;//изменив здесь единичку на другое число можно сделать джамп пад//строка колизии платформ сверху
                player._y = (Math.Abs(player._y) / 128) * 128 - 1;
            }
            if (map[(Math.Abs(player._y)) / 128, Math.Abs(player._x + 21) / 128] == 1)
            {
                player._x = player._x - 6;
                camera = true;
            }
            if (map[(Math.Abs(player._y)) / 128, Math.Abs(player._x - 22) / 128] == 1)
            {
                player._x = player._x + 6;
                camera = true;
            }
            if (map[Math.Abs(player._y-32) / 128, Math.Abs(player._x) / 128] == 1)
            {
                camera = true;
                player._y = player._y + 20;
            }
            if (map[(Math.Abs(player._y)) / 128, Math.Abs(player._x + 24) / 128] == 1)
            {
                player._x = player._x - 1;
                camera = true;
            }
            if (map[(Math.Abs(player._y)) / 128, Math.Abs(player._x - 25) / 128] == 1)
            {
                player._x = player._x + 1;
                camera = true;
            }


            switch (currAnimation)//camera
            {
                case 1:
                    if (player._x > this.Width / 2 - 3 && player._x < sideOfMapObject * width - this.Width / 2 && !camera)
                        //delta.X += 6;
                        delta.X -= (int)player._dir.X;
                    break;
                case 2:
                    if (player._x > this.Width / 2 + 3 && player._x <= sideOfMapObject * width - this.Width / 2 && !camera)
                        //delta.X -= 6;
                        delta.X -= (int)player._dir.X;
                    break;
                case 3:
                    if (player._x > this.Width / 2 - 3 && player._x < sideOfMapObject * width - this.Width / 2 && !camera)
                        delta.X += +6;
                    //delta.X += +player.speed;
                    break;
                case 4:
                    if (player._x > this.Width / 2 + 3 && player._x <= sideOfMapObject * width - this.Width / 2 && !camera)
                        delta.X -= +6;
                    //delta.X -= +player.speed;
                    break;
            }


            label1.Text = delta.X.ToString();
            label2.Text = delta.Y.ToString();
            label3.Text = player._x.ToString();
            label4.Text = player._y.ToString();

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

            if (currAnimation == 3)
            {
                player.currFrame = 1;
                currAnimation = 5;
            }

            if (currAnimation == 4)
            {
                player.currFrame = 1;
                currAnimation = 6;
            }
            player._dir.X = 0;

            if (player._x > this.Width / 2  && player._x < sideOfMapObject * width - this.Width / 2 && player._x+delta.X != this.Width / 2)
            {//фикс камеры при отпускании клавиши
                delta.X = this.Width / 2 - player._x;
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
                case "W":
                    if (currAnimation == 5)
                        currAnimation = 3;
                    if (currAnimation == 6)
                        currAnimation = 4;
                    break;
                case "Space":
                    if (currAnimation == 5)
                        currAnimation = 3;
                    if (currAnimation == 6)
                        currAnimation = 4;
                    break;
            }
            if (e.KeyCode.ToString() == "A" || e.KeyCode.ToString() == "D"|| e.KeyCode.ToString() == "W"|| e.KeyCode.ToString() == "Space")
            isPressedAnyKey = true;
        }

        private void update(object sender, EventArgs e)
        {

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
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(128 * player.currFrame, 128 * 1 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
                if (currAnimation == 2)
                {
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(2560 - 128 * player.currFrame, 128 * 1 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
                if (currAnimation == 3)
                {
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(128 * player.currFrame, 128 * 2 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
                if (currAnimation == 4)
                {
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(2560 - 128 * player.currFrame, 128 * 2 /*player.currAnimation*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
            }
            else
            {
                if (currAnimation == 5)
                {
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(128 * player.currFrame, 128 * 0 /*(player.currAnimation - 5)*/), new Size(128, 128)), GraphicsUnit.Pixel);
                }
                if (currAnimation == 6)
                {
                    gr.DrawImage(player._spritesAnimation, player._x + delta.X - 64, player._y + delta.Y - 127, new Rectangle(new Point(2560 - 128 * player.currFrame, 128 * 0 /*(player.currAnimation - 5)*/), new Size(128, 128)), GraphicsUnit.Pixel);
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
