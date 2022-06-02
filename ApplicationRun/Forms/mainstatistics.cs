
using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.ConditionalFormatting;
using OfficeOpenXml.Table;


namespace ApplicationRun.Forms
{

    public partial class mainstatistics : Form
    {
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "CALL Adminka_statistics_test('','','');";


        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод обращений на печать', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
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
                    string sql_search = $"CALL Adminka_statistics_test('" + toolStripTextBox1.Text + "','" + toolStripTextBox2.Text + "','" + "')";

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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        public mainstatistics()
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

        private void mainstatistics_FormClosing(object sender, FormClosingEventArgs e)
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
            this.Hide();
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Excel.Application exApp = new Excel.Application();
            //exApp.Workbooks.Add();
            //Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            //int i, j;
            //for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            //{
            //    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
            //    {
            //        wsh.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
            //    }
            //}


ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //var table1 = 
            int startRowT1 = 3;  //Для первого листа
            int startColT1 = 1;  //Для первого листа
            DateTime dt;
            double d;
             int i;int j;

            FileInfo template = new FileInfo("C:/Users/learner/Desktop/graduationwork/Tables/Обращения.xlsx");
            if (!template.Exists)
            {  //Делаем проверку - если Template.xlsx отсутствует - выходим по красной ветке
                //gradu.SendErrorToLog("Упс! Файл Excel-шаблона 'Template.xlsx' отсутствует в директории проекта.", true);
                //return null;
            }

            using (ExcelPackage exPack = new ExcelPackage(template, true))
            {
                //Тут будет весь дальнейший код. А именно:
                //Работа с 1 листом
                //Работа со 2 листом
                //Сохранение файла

                ExcelWorksheet ws1 = exPack.Workbook.Worksheets[0];
                //for (int row = startRowT1; row < dataGridView1.RowCount + startRowT1; row++)
                //{
                //    for (int col = startColT1; col < dataGridView1.ColumnCount + startColT1; col++)
                //    {
                //        ws1.Cells[row, col].Value = dataGridView1[col - startColT1, row - startRowT1];
                //    }
                //}
                for (i = 0; i <= dataGridView1.RowCount -1; i++)
                {
                    for (j = 0; j <= dataGridView1.ColumnCount -1; j++)
                    {
                        ws1.Cells[i + 3, j + 1].Value = dataGridView1[j, i].Value.ToString();
                    }
                }
                    Byte[] bin = exPack.GetAsByteArray();
                string resPath = "C:/Users/learner/Desktop/graduationwork/Tables/Result.xlsx";
                File.WriteAllBytes(resPath, bin);

            }

            
            
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
               // MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод отчёта обращений', surname, name, NOW() FROM tmp_authorization;", connection);
               // command_insert_log.ExecuteNonQuery();
            }
        }

        private void работаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            test Main = new test();
            Main.Show();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с анализом обращений', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }

        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        public int kodbileta;
        public int kodreysa;
        public int mesto;

        private void продатьБилетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                kodbileta = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value);
                kodreysa = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[1].Value);
                mesto = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[2].Value);

                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"UPDATE Билет SET Статус=1 WHERE [Код билета] = {kodbileta}", connection);
                    command.ExecuteScalar();
                }

                //Клиент Main = new Клиент();
                //Main.kodbileta = this.kodbileta;
                //Main.kodreysa = this.kodreysa;
                //Main.mesto = this.mesto;
                //Main.Show();
                //Main.bunifuButton3.Visible = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripTextBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    //string search = textBox1.ToString();
                    string sql_search = $"CALL Adminka_statistics_test('" + toolStripTextBox1.Text + "','" + toolStripTextBox2.Text + "','" + toolStripTextBox6 + "')";

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

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    connection.Open();
                    //string search = textBox1.ToString();
                    string sql_search = $"CALL Adminka_statistics_test('" + toolStripTextBox1.Text + "','" + toolStripTextBox2.Text + "','"  + "')";

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

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
