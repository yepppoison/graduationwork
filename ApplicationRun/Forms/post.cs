using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;


namespace ApplicationRun.Forms
{
    public partial class post : Form
    {
        
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "SELECT id '№', name_of_diagnosis 'Диагноз' FROM diagnosis";
        public post()
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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    //string search = textBox1.ToString();
                    string sql_search = $"SELECT id '№', name_of_diagnosis 'Диагноз' FROM diagnosis WHERE name_of_diagnosis LIKE ('" + toolStripTextBox1.Text + "%')";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql_search, connection);

                    ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void diagnosis_FormClosed(object sender, FormClosedEventArgs e)
        {


        }
    }
}
