using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class test : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=wpl36.hosting.reg.ru;" + "DATABASE=u1580638_graduationwork;" + "UID=u1580638_learner;" + "PASSWORD=Qxdm?779;" + "connection timeout = 180";
        string sql = "SELECT * FROM user";

       


        public test()
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
                dataGridView1.Columns[0].Width = 45;
                dataGridView1.Columns[1].Width = 45;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].Width = 65;
     
            }
        }
        }
}
