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


        private void btnEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            employee Main = new employee();
            Main.ShowDialog(); 
        }


        private void btnLog_Click(object sender, EventArgs e)
        {
            this.Hide();
            log Main=new log();
            Main.ShowDialog();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void btnConstant_Click(object sender, EventArgs e)
        {
            this.Hide();
            constants Main = new constants();
            Main.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            users Main = new users();
            Main.ShowDialog(); 
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            this.Hide();
            post Main = new post();
            Main.ShowDialog();
        }
    }
}
