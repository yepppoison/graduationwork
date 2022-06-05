using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class log : Form
    {
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "SELECT login 'Логин', move 'Действие', surname 'Фамилия', name 'Имя', dt 'Дата' FROM log WHERE dt>NOW()-INTERVAL 12 HOUR ORDER BY dt DESC;";

        public log()
        {
            InitializeComponent();
            dgvLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLog.AllowUserToAddRows = false;

            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvLog.DataSource = ds.Tables[0];
            }
        }

        private void log_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Admin Main = new Admin();
            Main.Show();
        }
    }
}
