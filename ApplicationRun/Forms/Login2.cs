using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationRun.Forms
{
    public partial class Login2 : Form
    {
        public Login2()
        {
            InitializeComponent();
        }
    }
}


//Global Varibles
//        int pw;
//bool hided;
//Initialize Variables: 
//            pw = panel5.Width;
//hided = false;

//Opacity Function/Methode
//        void IncreaseOpacity(object sender, EventArgs e)
//        {
//    if (this.Opacity leesthan = 1)  //replace 0.88 with whatever you want
//            {
//        this.Opacity += 0.01;  //replace 0.01 with whatever you want
//    }
//    if (this.Opacity == 1) //replace 0.88 with whatever you want
//        timer.Stop();
//}

//textBox1_TextChanged Event
//                if (textBox1.Text == "")
//                {
//    textBox1.Text = "Enter username";
//    return;
//}
//textBox1.ForeColor = Color.White;
//panel5.Visible = false;
//textBox2_TextChanged
//{
//    if (textBox2.Text == "")
//    {
//        return;
//    }
//    textBox2.ForeColor = Color.White;
//    textBox2.PasswordChar = '*'; // convert text to *
//    panel7.Visible = false;

//    textBox1_Click{
//        textBox1.SelectAll();
//        textBox2_Click{
//            textBox2.SelectAll();
//            button1_MouseEnter
//                    button1.ForeColor = Color.Black;
//            button1_MouseLeave
//                    button1.ForeColor = Color.Lime;
//            ******Dragging Import Files ********
       
//               [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
//                private extern static void ReleaseCapture();
//            [DllImport("user32.DLL", EntryPoint = "SendMessage")]
//            private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

//            ***************Form Load * ******************************************
//                private void Form1_Load(object sender, EventArgs e)
//            {
//                this.Opacity = .01;
//                timer.Interval = 10; //replace 10 with whatever you want
//                timer.Tick += IncreaseOpacity;
//                timer.Start();//timer1.Start();
//            }


//            linkLabel3_LinkClicked  {
//                pnlLogo.Dock = DockStyle.Left;
//                pnlsignup.Visible = false;
//                pnlLogin.Visible = true;
//                pnlLogin.Dock = DockStyle.Fill;

//            }

//            linkLabel2_LinkClicked {
//                pnlLogo.Dock = DockStyle.Right;
//                pnlLogin.Visible = false;
//                pnlsignup.Visible = true;
//                pnlsignup.Dock = DockStyle.Fill;
//            }
//            ****************Call Drag Function on Mouse down Event *******************
//                panel1_MouseDown
//                {
//                ReleaseCapture();
//                SendMessage(this.Handle, 0x112, 0xf012, 0);
//            }

//            private void Form1_DragEnter(object sender, DragEventArgs e)
//            {
//                this.Opacity = 0.5;
//            }

//            private void Form1_ResizeBegin(object sender, EventArgs e)
//            {
//                this.Opacity = 0.5;
//            }

//            private void Form1_ResizeEnd(object sender, EventArgs e)
//            { this.Opacity = 1;