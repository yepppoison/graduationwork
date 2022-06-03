using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class Login2 : Form
    {
        static string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        public Login2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text=="")
                { textBox1.Text = "Введите логин";
                    return;
                }
                textBox1.ForeColor = Color.Gray;
                panel5.Visible = false;
            }
            catch { }
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "")
                { textBox2.Text = "Введите пароль";
                    return;
                }
                textBox2.ForeColor = Color.Gray;
                panel7.Visible = false;
            }
            catch{ }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void bunifuButton7_MouseEnter(object sender, EventArgs e)
        {
            bunifuButton7.ForeColor = Color.Lime;
        }

        private void bunifuButton7_MouseLeave(object sender, EventArgs e)
        {
            bunifuButton7.ForeColor = Color.Lime;
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "Введите логин" || textBox1.Text == "") // or write you iwn Cindution
            {
                panel5.Visible = true;
                return;
            }
            if (textBox2.Text == "Введите пароль" || textBox2.Text == "") // or write you iwn Cindution
            {
                panel7.Visible = true;
                return;

            }
            string login = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    string sql = $"CALL authorization('" + textBox1.Text + "'" + ",'" + textBox2.Text + "')" + ";SELECT * FROM tmp_authorization";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    string sql_admin = $"SELECT * FROM tmp_authorization WHERE acces_name = 'Администратор'";

                    MySqlDataAdapter adapter_admin = new MySqlDataAdapter(sql_admin, connection);
                    DataSet ds_admin = new DataSet();
                    adapter_admin.Fill(ds_admin);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.Hide();
                        MainForm2 Main = new MainForm2();

                        MySqlCommand command1 = new MySqlCommand($"SELECT surname FROM tmp_authorization", connection);
                        MySqlCommand command2 = new MySqlCommand($"SELECT name FROM tmp_authorization", connection);
                        MySqlCommand command3 = new MySqlCommand($"SELECT name_post FROM tmp_authorization", connection);
                        Main.label1.Text = $"Авторизованный пользователь:\n{command1.ExecuteScalar().ToString()} {command2.ExecuteScalar().ToString()} - {command3.ExecuteScalar().ToString()}";

                        
          
                        if (ds_admin.Tables[0].Rows.Count > 0)
                        {
                            Main.iconButton6.Visible = true;
                        }
                        MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вход в программу', surname, name, NOW() FROM tmp_authorization;", connection);
                        command_insert_log.ExecuteNonQuery();
                        //Application.Run(new MainForm2());
                        Main.Show();
                        //MySqlCommand command_drop1 = new MySqlCommand($"DROP TEMPORARY TABLE IF EXISTS tmp_authorization", connection);
                        //command_drop1.ExecuteNonQuery();
                        //MessageBox.Show("Вход выполнен");
                    }
                    else { }
                        //MessageBox.Show("Неправильный логин/пароль");

                    //MySqlCommand command_drop2 = new MySqlCommand($"DROP TEMPORARY TABLE IF EXISTS tmp_authorization", connection);
                    //command_drop2.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Ошибка: нет доступа к базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                bunifuButton7.PerformClick();
            }

            if (e.KeyData == Keys.Up)
            {
                textBox1.Focus();
            }

            if (e.KeyData == Keys.Down)
            {
                textBox2.Focus();
            }
       
        }
    }
}

