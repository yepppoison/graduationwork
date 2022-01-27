using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ApplicationRun.Forms
{

    public partial class MainForm : Form
    {
        string connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=Аэропорт;" + "Integrated Security=SSPI;Pooling=False";
        Timer myTimer = new Timer();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Transaction = transaction;

                    try
                    {
                        command.CommandText = $"EXEC ТранзакцияБаланс";
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
                        label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($@"Исключение: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }                                     
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            myTimer.Interval = 5000;  // 5 сек
            myTimer.Enabled = true;
            myTimer.Tick += timer1_Tick;
            myTimer.Start();

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
                label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}";

            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            КлиентПредставление Main = new КлиентПредставление();
            Main.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            БилетПредставление Main = new БилетПредставление();
            Main.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Admin Main = new Admin();
            Main.Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Main = new Login();
            Main.Show();
        }


        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            СамолётПредставление Main = new СамолётПредставление();
            Main.Show();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            РейсПредставление Main = new РейсПредставление();
            Main.Show();
        }
    }
}
