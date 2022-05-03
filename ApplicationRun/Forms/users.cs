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
    public partial class users : Form
    {
        DataSet ds;
        MySqlDataAdapter adapter;

        MySqlCommandBuilder commandBuilder;
        string connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=root;" + "PASSWORD=dowhatthouwilt;" + "connection timeout = 180";
        string sql = "CALL usermiac();";
        public users()
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
            }
        }
    }
}
