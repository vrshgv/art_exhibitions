using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;
        HashCode hc = new HashCode();
         public Form1()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = constr;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = constr;
            con.Open();
            string Username = textBox1.Text;
            string password = textBox2.Text;
            OracleCommand cmd = new OracleCommand("select login, password from VER3.USERS where login='" + textBox1.Text + "' and password='" + hc.PassHash(textBox2.Text) + "'", con);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
             if (dt.Rows.Count > 0)
            {
                this.Hide();
                Form10 ss = new Form10();
                con.Close();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Wrong login or password");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ss1 = new Form2();
            ss1.Show();
        }
    }
}
