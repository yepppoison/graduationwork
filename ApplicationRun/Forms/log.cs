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
        }
    }
}
