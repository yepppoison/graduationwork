using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class Login : Form
    {
        string connectionString = @"SERVER=wpl36.hosting.reg.ru;" + "DATABASE=u1580638_graduationwork;" + "UID=u1580638_learner;" + "PASSWORD=Qxdm?779;" + "connection timeout = 180";

        public Login()
        {
            InitializeComponent();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();

                    string sql = $"SELECT * FROM user WHERE login = '" + textBox1.Text + "'" + "AND password = '" + textBox2.Text + "'";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    string sql_admin = $"SELECT * FROM authorization WHERE login = '" + textBox1.Text + "'" + "AND password = '" + textBox2.Text + "'" + "AND acces_name = 'Администратор'";

                    MySqlDataAdapter adapter_admin = new MySqlDataAdapter(sql_admin, connection);
                    DataSet ds_admin = new DataSet();
                    adapter_admin.Fill(ds_admin);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.Hide();
                        MainForm Main = new MainForm();   //CALL authorization

                        MySqlCommand command = new MySqlCommand($"SELECT surname FROM users WHERE login = '" + textBox1.Text + "'" + "AND password = '" + textBox2.Text + "'", connection);
                        MySqlCommand command2 = new MySqlCommand($"SELECT name FROM users WHERE login = '" + textBox1.Text + "'" + "AND password = '" + textBox2.Text + "'", connection);
                        MySqlCommand command3 = new MySqlCommand($"SELECT name_post FROM users WHERE login = '" + textBox1.Text + "'" + "AND password = '" + textBox2.Text + "'", connection);
                        Main.label1.Text = $"Авторизованный пользователь:\n{command.ExecuteScalar().ToString()} {command2.ExecuteScalar().ToString()} - {command3.ExecuteScalar().ToString()}";

                        Main.Show();
                        if (ds_admin.Tables[0].Rows.Count > 0)
                        {
                            Main.bunifuImageButton1.Enabled = true;
                        }

                        MessageBox.Show("Вход выполнен");
                    }
                    else
                        MessageBox.Show("Неправильный логин/пароль");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
