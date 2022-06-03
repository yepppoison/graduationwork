using System;
using System.Windows.Forms;

namespace ApplicationRun.Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_KeyDown(object sender, KeyEventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            users Main = new users();
            Main.Show(); 
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            log Main=new log();
            Main.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            this.Hide();
            employees Main = new employees();
            Main.Show();
        }
    }
}
