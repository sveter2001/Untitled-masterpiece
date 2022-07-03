using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Untitled_masterpiece
{
    public partial class Untitled_masterpiece : Form
    {
        public Untitled_masterpiece()
        {
            InitializeComponent();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Start_Click(object sender, EventArgs e)
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
