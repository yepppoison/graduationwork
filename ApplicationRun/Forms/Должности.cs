﻿using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace ApplicationRun.Forms
{
    public partial class Должности : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=Аэропорт;" + "Integrated Security=SSPI;Pooling=False";
        string sql = "SELECT * FROM Должности";

        public Должности()
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
                dataGridView1.Columns[0].Width = 70;
                dataGridView1.Columns[1].Width = 95;
                dataGridView1.Columns[2].Width = 60;
                dataGridView1.Columns[3].Width = 80;
            }
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

        private void Должности_FormClosing(object sender, FormClosingEventArgs e)
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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    string sql_search = $"SELECT * FROM Должности WHERE [{toolStripComboBox1.Text}] LIKE '" + toolStripTextBox1.Text + "%'";
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
