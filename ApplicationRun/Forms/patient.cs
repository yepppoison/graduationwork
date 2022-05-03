using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ApplicationRun.Forms
{

    public partial class patient : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;

        SqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "SELECT * FROM patient";


        public patient()
        {
         
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            toolStripComboBox1.Text = toolStripComboBox1.Items[0].ToString();
            toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 45;
                dataGridView1.Columns[1].Width = 45;
                dataGridView1.Columns[2].Width = 45;
                dataGridView1.Columns[3].Width = 55;
                dataGridView1.Columns[4].Width = 50;
            }
        }

        private void  patient_FormClosing(object sender, FormClosingEventArgs e)
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

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); 
            ds.Tables[0].Rows.Add(row);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(sql, connection);
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

        private void toolStripTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    string sql_search = $"SELECT * FROM БилетКлиента WHERE [{toolStripComboBox1.Text}] LIKE '" + toolStripTextBox1.Text + "%'";
                    adapter = new SqlDataAdapter(sql_search, connection);
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
    }
}
