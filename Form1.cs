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
        Image playerImg_L;
        Image playerImg_R;
        private int currFrame = 0;
        private int currAnimation = 1;
        public Untitled_masterpiece()
        {
            InitializeComponent();
            // Используйте двойную буферизацию для уменьшения мерцания.
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            playerImg_L = new Bitmap("D:\\slime.png");
            playerImg_R = new Bitmap("D:\\slimeR.png");



            timer1.Interval = 100;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            this.KeyDown += new KeyEventHandler(keyboard);





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

            }
        }

        private void update(object sender, EventArgs e)
        {
            playAnimation();
            if (currFrame == 9)
                currFrame = 0;
            currFrame++;
        }

        private void playAnimation()
        {
            if (currAnimation == 1)
            {
                Image part = new Bitmap(128, 128);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(playerImg_L, 0, 0, new Rectangle(new Point(128 * currFrame, 128 * 2), new Size(128, 128)), GraphicsUnit.Pixel);
                pictureBox1.Size = new Size(128, 128);
                pictureBox1.Image = part;
            }
            else
            {
                Image part = new Bitmap(128, 128);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(playerImg_R, 0, 0, new Rectangle(new Point(1280 - 128* currFrame, 128 * 2), new Size(128, 128)), GraphicsUnit.Pixel);
                pictureBox1.Size = new Size(128, 128);
                pictureBox1.Image = part;
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


        public class Mumu
        {
            
            
        }

       
    }
}
