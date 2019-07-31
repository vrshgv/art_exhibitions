using System;
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
using System.Configuration;
using System.IO;
namespace WindowsFormsApp1
{
    public partial class Form10 : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;
        HashCode hc = new HashCode();
        public Form10()
        {
            InitializeComponent();
        }
        OracleConnection con = new OracleConnection();
        
        string imgLocation = "";
        OracleCommand cmd;
        private void Form10_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.PARTICIPANTS1". При необходимости она может быть перемещена или удалена.
            this.pARTICIPANTS1TableAdapter.Fill(this.dataSet2.PARTICIPANTS1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.EXHIBITION". При необходимости она может быть перемещена или удалена.
            this.eXHIBITIONTableAdapter.Fill(this.dataSet2.EXHIBITION);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ARTIST". При необходимости она может быть перемещена или удалена.
            this.aRTISTTableAdapter.Fill(this.dataSet2.ARTIST);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ARTWORK". При необходимости она может быть перемещена или удалена.
            this.aRTWORKTableAdapter.Fill(this.dataSet2.ARTWORK);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ARTWORK". При необходимости она может быть перемещена или удалена.
            this.aRTWORKTableAdapter.Fill(this.dataSet2.ARTWORK);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.PLACE". При необходимости она может быть перемещена или удалена.
            this.pLACETableAdapter.Fill(this.dataSet2.PLACE);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.PARTICIPANTS". При необходимости она может быть перемещена или удалена.
            this.pARTICIPANTSTableAdapter.Fill(this.dataSet2.PARTICIPANTS);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.EXHIBITION1". При необходимости она может быть перемещена или удалена.
            this.eXHIBITION1TableAdapter.Fill(this.dataSet2.EXHIBITION1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ARTWORK1". При необходимости она может быть перемещена или удалена.
            this.aRTWORK1TableAdapter.Fill(this.dataSet2.ARTWORK1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.testGr". При необходимости она может быть перемещена или удалена.
            this.testGrTableAdapter.Fill(this.dataSet2.testGr);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.exhibitiontest". При необходимости она может быть перемещена или удалена.
            

        }

        private void testGrDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            con.ConnectionString = constr;
            con.Open();
            int exh_id = int.Parse(eXHIBITION_IDTextBox.Text);
            string exh_name = eXHIBITION_NAMETextBox.Text;
            DateTime date = this.eXDATEDateTimePicker.Value;
            int place_id = int.Parse(pLACE_IDTextBox.Text);
            string direction = dIRECTIONTextBox.Text;
            string descr = eXHIBITION_DESCRIPTIONTextBox.Text;

            
            OracleCommand cmd = new OracleCommand("INSERT INTO VER3.EXHIBITION(EXHIBITION_ID, EXHIBITION_NAME, EXHIBITION_DESCRIPTION, PLACE_ID, DIRECTION, EXDATE) VALUES('" + exh_id+ "', '" + exh_name + "', '" + descr +"', '" + place_id +"', '" + direction +"',:dateParam)", con);
           
     
            cmd.Parameters.Add(new OracleParameter("dateParam", this.eXDATEDateTimePicker.Value));
            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Exhibition saved successfully!");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Oops, wrong. Check if you filled all the fields correctly:"+" "+exc.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pICTUREPictureBox.ImageLocation = imgLocation;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream Streem = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(Streem);
            images = brs.ReadBytes((int)Streem.Length);

            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "INSERT INTO VER3.ARTWORK(ARTWORK_ID,TITLE,COST,PLACE_ID,ARTIST_ID,YEAR_OF_CREATION,PICTURE) VALUES('" + int.Parse(aRTWORK_IDTextBox.Text) + "', '" + tITLETextBox.Text + "', '" + cOSTTextBox.Text + "', '" + int.Parse(pLACE_IDTextBox1.Text) + "', '" + int.Parse(aRTIST_IDTextBox.Text) + "', '" + int.Parse(yEAR_OF_CREATIONTextBox.Text) + "',:images) ";
                cmd = new OracleCommand(sqlQuery, con);
                cmd.Parameters.Add(new OracleParameter("images", images));
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Artwork saved successfully!");
            }catch(Exception exc)
            {
                MessageBox.Show("Oops, wrong. Check if you filled all the fields correctly:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

        }

        private void bindingNavigator3_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "INSERT INTO VER3.PARTICIPANTS(PARTICIPANT_ID,EXHIBITION_ID,ARTIST_ID) VALUES('" + int.Parse(pARTICIPANT_IDTextBox1.Text) + "', '"+ int.Parse(eXHIBITION_IDTextBox2.Text) + "', '" + int.Parse(aRTIST_IDTextBox2.Text) + "') ";
                cmd = new OracleCommand(sqlQuery, con);
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Participation saved successfully!");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Oops, wrong. Check if you filled all the fields correctly:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            
            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "DELETE FROM VER3.ARTWORK WHERE ARTWORK_ID=:ARTID";
                cmd = new OracleCommand(sqlQuery, con);
                cmd.Parameters.Add(new OracleParameter("ARTID", int.Parse(aRTWORK_IDTextBox.Text)));
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("The entry from ARTWORK was successfully deleted.");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Deletion is failed:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void bindingNavigatorDeleteItem3_Click(object sender, EventArgs e)
        {
            
            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "DELETE FROM VER3.PARTICIPANTS WHERE PARTICIPANT_ID=:PARID";
                cmd = new OracleCommand(sqlQuery, con);
                cmd.Parameters.Add(new OracleParameter("PARID", int.Parse(pARTICIPANT_IDTextBox1.Text)));
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("The entry from PARTICIPANTS was successfully deleted.");
            }
            catch (Exception exc)
            {
        MessageBox.Show("Deletion is failed:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
           
            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "DELETE FROM VER3.EXHIBITION WHERE EXHIBITION_ID=:EXID";

                cmd = new OracleCommand(sqlQuery, con);
                cmd.Parameters.Add(new OracleParameter("EXID", eXHIBITION_IDTextBox.Text));
               
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("The entry from EXHIBITION was successfully deleted.");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Deletion is failed:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = constr;
                con.Open();
                string sqlQuery = "INSERT INTO VER3.USERS(USER_ID,LOGIN,PASSWORD) VALUES('" + int.Parse(textBox3.Text) + "', '" + textBox1.Text + "', '" + hc.PassHash(textBox2.Text) + "') ";
                cmd = new OracleCommand(sqlQuery, con);
                int N = cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User saved successfully!");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Oops, wrong. Check if you filled all the fields correctly:" + " " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
