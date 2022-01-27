using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace ApplicationRun.Forms
{
    public partial class РейсПредставление : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=HOME\SQLEXPRESS;Initial Catalog=Аэропорт;" + "Integrated Security=SSPI;Pooling=False";

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    string sql_search = $"SELECT * FROM РейсПредставление WHERE [{toolStripComboBox1.Text}] LIKE '" + toolStripTextBox1.Text + "%'";
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

        private void работаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Рейс Main = new Рейс();
            Main.Show();
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            int i, j;
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    wsh.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }
            exApp.Visible = true;
        }

        private void печатьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }


        private void Рейсы_FormClosing(object sender, FormClosingEventArgs e)
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

        string sql = "SELECT * FROM РейсПредставление";

        public РейсПредставление()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            toolStripComboBox1.Text = toolStripComboBox1.Items[0].ToString();
            toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            using SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 35;
            }
        }
    }
}
