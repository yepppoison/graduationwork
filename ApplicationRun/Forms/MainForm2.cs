
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using ApplicationRun.DashboardNamespace;

namespace ApplicationRun.Forms
{
    public partial class MainForm2 : Form
    {
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string appeal = "CALL Adminka_statistics_test('','','');";
        string hostipal = "CALL hospital();";
        string statistic = "CALL test('','');";
        string diagnos = "SELECT name_of_diagnosis 'Диагноз' FROM diagnosis";
        //Fields
        private int borderSize = 2;
        private Size formSize; //Сохраняйте размер формы при ее сворачивании и восстановлении. Поскольку размер формы изменяется, поскольку учитывается размер строки заголовка и границ.


        //Fields
        private Dashboard model;
        private Button currentButton;



        public bool UserSuccessfulAuthenticated { get; internal set; }

        //Consturctor
        public MainForm2()
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize); //Размер границы
            this.BackColor = Color.SteelBlue; //Цвет границы

            //Default - Last 7 days
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();
            SetDateMenuButtonsUI(btnLast7Days);
            model = new Dashboard();
            LoadData();

        }

        private void LoadData()
        {
            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lblNumOrders.Text = model.NumOrders.ToString();
                lblTotalRevenue.Text = "$" + model.TotalRevenue.ToString();
                lblTotalProfit.Text = "$" + model.TotalProfit.ToString();
                lblNumCustomers.Text = model.NumCustomers.ToString();
                lblNumSuppliers.Text = model.NumSuppliers.ToString();
                lblNumProducts.Text = model.NumProducts.ToString();
                chartGrossRevenue.DataSource = model.GrossRevenueList;
                chartGrossRevenue.Series[0].XValueMember = "Date";
                chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
                chartGrossRevenue.DataBind();
                chartTopProducts.DataSource = model.TopProductsList;
                chartTopProducts.Series[0].XValueMember = "Key";
                chartTopProducts.Series[0].YValueMembers = "Value";
                chartTopProducts.DataBind();
                //dgvUnderstock.DataSource = model.UnderstockList;
                //dgvUnderstock.Columns[0].HeaderText = "Item";
                //dgvUnderstock.Columns[1].HeaderText = "Units";
                Console.WriteLine("Loaded view :)");
            }
            else Console.WriteLine("View not loaded, same query");
        }
        private void SetDateMenuButtonsUI(object button)
        {
            var btn = (Button)button;
            //Highlight button
            btn.BackColor = btnLast30Days.FlatAppearance.BorderColor;
            btn.ForeColor = Color.White;
            //UnhighLigh button
            if(currentButton != null&&currentButton!=btn)
            {
                currentButton.BackColor = Color.LightBlue;
                currentButton.ForeColor = Color.FromArgb(124, 141, 181);
            }
            currentButton = btn; //Set current button

            //Enable custom dates
            if(btn==btnCustomDate)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
                btnOkCustomDate.Visible = true;
                lblStartDate.Cursor = Cursors.Hand;
                lblEndDate.Cursor = Cursors.Hand;

            }

            //Disable custom dates
            else
            {

                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
                btnOkCustomDate.Visible = false;
                lblStartDate.Cursor = Cursors.Default;
                lblEndDate.Cursor = Cursors.Default;
            }


        }
        //Event methods
        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetDateMenuButtonsUI(sender);
        }
        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetDateMenuButtonsUI(sender);
        }
        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetDateMenuButtonsUI(sender);
        }
        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            SetDateMenuButtonsUI(sender);
        }
        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            SetDateMenuButtonsUI(sender);
        }
        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    

    //Перетаскивание формы
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        //Переопределенные методы
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Стандартная строка заголовка — окно привязки
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Свернуть форму (до)
            const int SC_RESTORE = 0xF120; //Восстановить форму (до)
            const int WM_NCHITTEST = 0x0084;//Win32, Уведомление о вводе с помощью мыши: определяет, какая часть окна соответствует точке, позволяет изменять размер формы.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Представляет клиентскую область окна
            const int HTLEFT = 10;  //Левая граница окна, позволяет изменить размер по горизонтали влево
            const int HTRIGHT = 11; //Правая граница окна, позволяет изменить размер по горизонтали вправо
            const int HTTOP = 12;   //Верхняя горизонтальная граница окна, позволяет изменять размер по вертикали вверх
            const int HTTOPLEFT = 13;//Верхний левый угол границы окна, позволяет изменить размер по диагонали влево
            const int HTTOPRIGHT = 14;//Верхний правый угол границы окна, позволяет изменить размер по диагонали вправо
            const int HTBOTTOM = 15; //Нижняя горизонтальная граница окна, позволяет изменять размер по вертикали вниз
            const int HTBOTTOMLEFT = 16;//Нижний левый угол границы окна, позволяет изменить размер по диагонали влево
            const int HTBOTTOMRIGHT = 17;//Нижний правый угол границы окна, позволяет изменить размер по диагонали вправо
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Измените размер формы, если она находится в нормальном состоянии
                {
                    if ((int)m.Result == HTCLIENT)//Если результат m (указатель мыши) находится в клиентской области окна
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Получает координаты точки экрана (координаты X и Y указателя)                         
                        Point clientPoint = this.PointToClient(screenPoint); //Вычисляет положение точки экрана в клиентские координаты                       
                        if (clientPoint.Y <= resizeAreaSize)//Если указатель находится в верхней части формы (в пределах области изменения размера - координата X)
                        {
                            if (clientPoint.X <= resizeAreaSize) //Если указатель находится в координате X=0 или меньше области изменения размера (X=10) в
                                m.Result = (IntPtr)HTTOPLEFT; //Изменить размер по диагонали влево
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//Если указатель находится в координате X=11 или меньше ширины формы (X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Изменить размер по диагонали вправо
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //Если указатель находится внутри формы по координате Y (без учета размера области изменения размера)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Изменить размер по горизонтали влево
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Изменить размер по горизонтали вправо
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Изменить размер по диагонали влево
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Изменить размер по вертикали вниз
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Изменить размер по диагонали вправо
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Удалить границу и сохранить окно привязки
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Сохраняйте размер формы при ее сворачивании и восстановлении. Поскольку форма изменена, потому что она учитывает размер строки заголовка и границ.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Цитировать:
                /// В сообщениях WM_SYSCOMMAND четыре малых бита параметра wParam
                /// используются внутри системы. Для получения правильного результата при тестировании
                /// значение wParam, приложение должно комбинировать значение 0xFFF0 с
                /// значением wParam с помощью побитового оператора AND.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        
        private void MainForm2_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Восстановленная форма (До)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Восстановленная форма (После)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }
        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) //Свернуть меню
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                btnMenu.Dock = DockStyle.Top;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            { //Развернуть меню
                panelMenu.Width = 230;
                pictureBox1.Visible = true;
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
           
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                //MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                //command_insert_log.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(appeal, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvUnderstock.DataSource = ds.Tables[0];
                dgvUnderstock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUnderstock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dataGridView1.Columns[0].Width = 100;
            }
            dgvUnderstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnderstock.AllowUserToAddRows = false;
            
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                //MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                //command_insert_log.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(hostipal, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvUnderstock.DataSource = ds.Tables[0];
                dgvUnderstock.Columns[0].Width = 200;
                dgvUnderstock.Columns[2].Width = 145;
                dgvUnderstock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUnderstock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvUnderstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnderstock.AllowUserToAddRows = false;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            
            //using MySqlConnection connection = new MySqlConnection(connectionString);
            //{
            //    connection.Open();
            //    //MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
            //    //command_insert_log.ExecuteNonQuery();
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(subject, connection);
            //    ds = new DataSet();
            //    adapter.Fill(ds);
            //    dataGridView1.DataSource = ds.Tables[0];
            //}
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.AllowUserToAddRows = false;
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
          
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                //MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                //command_insert_log.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(statistic, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvUnderstock.DataSource = ds.Tables[0];
                dgvUnderstock.Columns[0].Width = 70;
                dgvUnderstock.Columns[1].Width = 70;
                dgvUnderstock.Columns[2].Width = 75;
                dgvUnderstock.Columns[03].Width = 115;
                dgvUnderstock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUnderstock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvUnderstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnderstock.AllowUserToAddRows = false;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
   
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                //MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Работа с обращениями', surname, name, NOW() FROM tmp_authorization;", connection);
                //command_insert_log.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(diagnos, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dgvUnderstock.DataSource = ds.Tables[0];
                dgvUnderstock.Columns[0].Width = 430;
                dgvUnderstock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUnderstock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvUnderstock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnderstock.AllowUserToAddRows = false;
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            
            Admin Main = new Admin();
            Main.ShowDialog();
        }

        private void MainForm2_Load(object sender, EventArgs e)
        {
            lblStartDate.Text = dtpStartDate.Text;
            lblEndDate.Text = dtpEndDate.Text;
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login2 Main = new Login2();
            Main.ShowDialog();
        }

      

        private void iconButton2_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void iconButton2_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void iconButton2_LocationChanged(object sender, EventArgs e)
        {
           
        }

        private void iconButton2_MouseCaptureChanged(object sender, EventArgs e)
        {
           
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод обращений на печать', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 
            //int startRowT1 = 3;  //Для первого листа
            //int startColT1 = 1;  //Для первого листа
            //DateTime dt;
            //double d;
            //int i; int j;

            //FileInfo template = new FileInfo("C:/Users/learner/Desktop/graduationwork/Tables/Обращения.xlsx");
            //if (!template.Exists)
            //{  //Делаем проверку - если Template.xlsx отсутствует - выходим по красной ветке
            //    //gradu.SendErrorToLog("Упс! Файл Excel-шаблона 'Template.xlsx' отсутствует в директории проекта.", true);
            //    //return null;
            //}

            //using (ExcelPackage exPack = new ExcelPackage(template, true))
            //{
            //    //Тут будет весь дальнейший код. А именно:
            //    //Работа с 1 листом
            //    //Работа со 2 листом
            //    //Сохранение файла

            //    ExcelWorksheet ws1 = exPack.Workbook.Worksheets[0];
            //    for (i = 0; i <= dgvUnderstock.RowCount - 1; i++)
            //    {
            //        for (j = 0; j <= dgvUnderstock.ColumnCount - 1; j++)
            //        {
            //            ws1.Cells[i + 3, j + 1].Value = dgvUnderstock[j, i].Value.ToString();
            //        }
            //    }
            //    Byte[] bin = exPack.GetAsByteArray();
            //    string resPath = "C:/Users/learner/Desktop/graduationwork/Tables/Result.xlsx";
            //    File.WriteAllBytes(resPath, bin);

            //}



            //using MySqlConnection connection = new MySqlConnection(connectionString);
            //{
            //    connection.Open();
            //     MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод отчёта обращений', surname, name, NOW() FROM tmp_authorization;", connection);
            //     command_insert_log.ExecuteNonQuery();
            //}
        }

        private void lblStartDate_Click(object sender, EventArgs e)
        {
            if(currentButton==btnCustomDate)
            {
                dtpStartDate.Select();
                SendKeys.Send("%{DOWN}");
            }    
        }

        private void lblEndDate_Click(object sender, EventArgs e)
        {
            if (currentButton == btnCustomDate)
            {
                dtpEndDate.Select();
                SendKeys.Send("%{DOWN}");
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            lblStartDate.Text = dtpStartDate.Text;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            lblEndDate.Text = dtpEndDate.Text;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            int startRowT1 = 3;  //Для первого листа
            int startColT1 = 1;  //Для первого листа
            DateTime dt;
            double d;
            int i; int j;

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
                for (i = 0; i <= dgvUnderstock.RowCount - 1; i++)
                {
                    for (j = 0; j <= dgvUnderstock.ColumnCount - 1; j++)
                    {
                        ws1.Cells[i + 3, j + 1].Value = dgvUnderstock[j, i].Value.ToString();
                    }
                }
                Byte[] bin = exPack.GetAsByteArray();
                string resPath = "C:/Users/learner/Desktop/graduationwork/Tables/Result.xlsx";
                File.WriteAllBytes(resPath, bin);

            }



            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод отчёта обращений', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            {
                connection.Open();
                MySqlCommand command_insert_log = new MySqlCommand($"INSERT INTO log (login, move, surname, name, dt) SELECT login, 'Вывод обращений на печать', surname, name, NOW() FROM tmp_authorization;", connection);
                command_insert_log.ExecuteNonQuery();
            }
        }

        private void MainForm2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MainForm2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                reference Main = new reference();
                Main.ShowDialog();
            }

        }
    }
}
