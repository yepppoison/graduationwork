using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ApplicationRun.Forms
{
    public partial class Login : Form
    {
        string connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=Аэропорт;" + "Integrated Security=SSPI;Pooling=False";

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
            string pass = textBox2.Text;

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();

                    string sql = $"SELECT * FROM Пользователи WHERE login = '" + textBox1.Text + "'" + "AND pass = '" + textBox2.Text + "'";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    string sql_admin = $"SELECT * FROM ЛогинАдмин WHERE login = '" + textBox1.Text + "'" + "AND pass = '" + textBox2.Text + "'" + "AND Администрирование = 1";

                    SqlDataAdapter adapter_admin = new SqlDataAdapter(sql_admin, connection);
                    DataSet ds_admin = new DataSet();
                    adapter_admin.Fill(ds_admin);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.Hide();
                        MainForm Main = new MainForm();

                        SqlCommand command = new SqlCommand($"SELECT Фамилия FROM ПользователиПредставление WHERE login = '" + textBox1.Text + "'" + "AND pass = '" + textBox2.Text + "'", connection);
                        SqlCommand command2 = new SqlCommand($"SELECT Имя FROM ПользователиПредставление WHERE login = '" + textBox1.Text + "'" + "AND pass = '" + textBox2.Text + "'", connection);
                        SqlCommand command3 = new SqlCommand($"SELECT Должность FROM ПользователиПредставление WHERE login = '" + textBox1.Text + "'" + "AND pass = '" + textBox2.Text + "'", connection);
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
