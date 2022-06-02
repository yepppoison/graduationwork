using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class Login : Form
    {

        static string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
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
                    string sql= $"CALL authorization('" + textBox1.Text + "'" + ",'" + textBox2.Text + "')"+";SELECT * FROM tmp_authorization";
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
                        MainForm Main = new MainForm();   

                        MySqlCommand command1 = new MySqlCommand($"SELECT surname FROM tmp_authorization", connection);
                        MySqlCommand command2 = new MySqlCommand($"SELECT name FROM tmp_authorization", connection);
                        MySqlCommand command3 = new MySqlCommand($"SELECT name_post FROM tmp_authorization", connection);
                        Main.label1.Text = $"Авторизованный пользователь:\n{command1.ExecuteScalar().ToString()} {command2.ExecuteScalar().ToString()} - {command3.ExecuteScalar().ToString()}";

                        Main.Show();
                        if (ds_admin.Tables[0].Rows.Count > 0)
                        {
                            Main.bunifuButton5.Visible = true;
                        }
                        MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вход в программу', surname, name, NOW() FROM tmp_authorization;", connection);
                        command_insert_log.ExecuteNonQuery();
                        //MySqlCommand command_drop1 = new MySqlCommand($"DROP TEMPORARY TABLE IF EXISTS tmp_authorization", connection);
                        //command_drop1.ExecuteNonQuery();
                        MessageBox.Show("Вход выполнен");
                    }
                    else
                        MessageBox.Show("Неправильный логин/пароль");

                    //MySqlCommand command_drop2 = new MySqlCommand($"DROP TEMPORARY TABLE IF EXISTS tmp_authorization", connection);
                    //command_drop2.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Ошибка: нет доступа к базе данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
