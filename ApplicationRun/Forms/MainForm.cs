using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
     // ЭТА ГЛАВНАЯ
     //СДЕЛАТЬ НЕ КЛИКАБЕЛЬНОЙ
     // ПЕРЕМЕННЫЕ НАЗВАТЬ НОРМАЛЬНО
    public partial class MainForm : Form
    {
        public DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "CALL test('','');";

        //string connectionString = @"SERVER=wpl36.hosting.reg.ru;" + "DATABASE=u1580638_graduationwork;" + "UID=u1580638_learner;" + "PASSWORD=Qxdm?779;" + "connection timeout = 180";
        Timer myTimer = new Timer();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Login Main = new Login();
            Main.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            //try
            //{
            //    using SqlConnection connection = new SqlConnection(connectionString);
            //    {
            //        connection.Open();
            //        SqlTransaction transaction = connection.BeginTransaction();
            //        SqlCommand command = connection.CreateCommand();
            //        command.Transaction = transaction;

            //        try
            //        {
            //            command.CommandText = $"EXEC ТранзакцияБаланс";
            //            command.ExecuteNonQuery();
            //            transaction.Commit();
            //            SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
            //            label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}"; //Счетчик общего количество зараженных
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($@"Исключение: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            transaction.Rollback();
            //        }                                     
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public MainForm()
        {
            InitializeComponent();
            myTimer.Interval = 5000;  // 5 сек
            myTimer.Enabled = true;
            myTimer.Tick += timer1_Tick;
            myTimer.Start();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }


            //using SqlConnection connection = new SqlConnection(connectionString);
            //{
            //    connection.Open();
            //    SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
            //    label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}";

            //}
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            medical_institution Main = new medical_institution();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Просмотр мед. учреждений', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            mainstatistics Main = new mainstatistics();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
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
           
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            type_diagnosis Main = new type_diagnosis();
            Main.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
           // employees Main = new employees();
           // Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с пользователями', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            patient Main = new patient();
            Main.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            convolution Main = new convolution();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Просмотр сотрудников онлайн', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            log Main = new log();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Просмотр журнала посещений', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton6_Click_2(object sender, EventArgs e)
        {
            //ActiveForm.Enabled = false;
            diagnosis Main = new diagnosis();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с диагнозами', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Просмотр субъектов РФ', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            Admin Main = new Admin();
            Main.ShowDialog();
        }

        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Login Main = new Login();
            Main.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

         void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            mainstatistics Main = new mainstatistics();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton3_Click_2(object sender, EventArgs e)
        {
            medical_institution Main = new medical_institution();
            Main.ShowDialog();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Просмотр мед. учреждений', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
