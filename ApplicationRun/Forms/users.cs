using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ApplicationRun.Forms
{
    public partial class users : Form
    {
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "CALL useradmin()";
        public users()
        {
            InitializeComponent();
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AllowUserToAddRows = false;

            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvUsers.DataSource = ds.Tables[0];
            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvUsers.SelectedRows)
            {
                dgvUsers.Rows.Remove(row);
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    adapter = new MySqlDataAdapter(sql, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    dgvUsers.DataSource = ds.Tables[0];
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void users_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter_new_1 = new MySqlDataAdapter(sql, connection);
                    MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter_new_1);
                    adapter_new_1.Update(ds);
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // this.Hide();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
