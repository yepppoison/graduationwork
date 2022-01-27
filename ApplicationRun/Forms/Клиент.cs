using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ApplicationRun.Forms
{
    public partial class Клиент : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=Аэропорт;" + "Integrated Security=SSPI;Pooling=False";
        string sql = "SELECT * FROM Клиент";

        public Клиент()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 75;
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 90;
                dataGridView1.Columns[4].Width = 70;
            }
        }

        private void Клиент_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    SqlDataAdapter adapter_new_1 = new SqlDataAdapter(sql, connection);
                    commandBuilder = new SqlCommandBuilder(adapter_new_1);
                    adapter_new_1.Update(ds);
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Hide();
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

        public int kodklienta;
        public int kodbileta;
        public int kodreysa;
        public int mesto;

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            kodklienta = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value);
            БилетКлиента Main = new БилетКлиента();
            Main.Show();
            DataRow row =Main.ds.Tables[0].NewRow();
            Main.ds.Tables[0].Rows.Add(row);
            Main.dataGridView1[0, Main.dataGridView1.Rows.Count - 1].Value = kodbileta;
            Main.dataGridView1[2, Main.dataGridView1.Rows.Count - 1].Value = mesto;
            Main.dataGridView1[3, Main.dataGridView1.Rows.Count - 1].Value = kodklienta;
            Main.dataGridView1[4, Main.dataGridView1.Rows.Count - 1].Value = kodreysa;
            this.Hide();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    string sql_search = $"SELECT * FROM Клиент WHERE [{toolStripComboBox1.Text}] LIKE '" + toolStripTextBox1.Text + "%'";
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
