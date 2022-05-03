﻿using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ApplicationRun.Forms
{

    public partial class MainForm : Form
    {
        public DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";

        //string connectionString = @"SERVER=wpl36.hosting.reg.ru;" + "DATABASE=u1580638_graduationwork;" + "UID=u1580638_learner;" + "PASSWORD=Qxdm?779;" + "connection timeout = 180";
        Timer myTimer = new Timer();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            //try
            //{
            //    using SqlConnection connection = new SqlConnection(connectionString);
            //    {
            //        connection.Open();
            //        SqlTransaction transaction = connection.BeginTransaction();
            //        SqlCommand command = connection.CreateCommand();
            //        command.Transaction = transaction;

            //        try
            //        {
            //            command.CommandText = $"EXEC ТранзакцияБаланс";
            //            command.ExecuteNonQuery();
            //            transaction.Commit();
            //            SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
            //            label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}"; //Счетчик общего количество зараженных
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($@"Исключение: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            transaction.Rollback();
            //        }                                     
            //    }
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show($@"Исключение: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public MainForm()
        {
            InitializeComponent();
            myTimer.Interval = 5000;  // 5 сек
            myTimer.Enabled = true;
            myTimer.Tick += timer1_Tick;
            myTimer.Start();

            //using SqlConnection connection = new SqlConnection(connectionString);
            //{
            //    connection.Open();
            //    SqlCommand command2 = new SqlCommand("SELECT * FROM Баланс", connection);
            //    label2.Text = $"Баланс:\n{command2.ExecuteScalar().ToString()}";

            //}
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            medical_institution Main = new medical_institution();
            Main.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            БилетПредставление Main = new БилетПредставление();
            Main.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Admin Main = new Admin();
            Main.Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Main = new Login();
            Main.Show();
        }


        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            statistics Main = new statistics();
            Main.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            users Main = new users();
            Main.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            patient Main = new patient();
            Main.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
