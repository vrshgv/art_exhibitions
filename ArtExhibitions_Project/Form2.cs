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
using System.Diagnostics;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string constr = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;
        BindingSource bs;
        DataTable dt;
        Object source;
        BindingSource bs2;
        DataTable dt2;
        Object source2;
        public Form2()
        {
            InitializeComponent();
            radioButton1.CheckedChanged += new EventHandler(mylistener);
            radioButton2.CheckedChanged += new EventHandler(mylistener);
           textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ARTWORK1". При необходимости она может быть перемещена или удалена.
            this.aRTWORK1TableAdapter.Fill(this.dataSet2.ARTWORK1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.testGr". При необходимости она может быть перемещена или удалена.

            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet21.exhibitiontest". При необходимости она может быть перемещена или удалена.
            this.exhibitiontestTableAdapter.Fill(this.dataSet2.exhibitiontest);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.ArtistWithArtwork". При необходимости она может быть перемещена или удалена.

            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.exhibitiontest". При необходимости она может быть перемещена или удалена.
            this.testGrTableAdapter.Fill(this.dataSet2.testGr);

            total();
            israr();
         
            bs = (BindingSource)exhibitiontestDataGridView.DataSource;
            dt = (bs.DataSource as DataSet).Tables[bs.DataMember];
             source = exhibitiontestDataGridView.DataSource;
            bs2 = (BindingSource)testGrDataGridView.DataSource;
            dt2 = (bs2.DataSource as DataSet).Tables[bs.DataMember];
            source2 = testGrDataGridView.DataSource;
        }
        void total()
        {
            for(int i=0; i < exhibitiontestDataGridView.Rows.Count - 1; i++)
            {
                DateTime a = Convert.ToDateTime(exhibitiontestDataGridView.Rows[i].Cells[2].Value);
                DateTime b = DateTime.Now;
                double dayToEvent = (a - b).Days;
                exhibitiontestDataGridView.Rows[i].Cells[8].Value = dayToEvent;
            }
        }
        void israr()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = constr;
            con.Open();
            for (int i = 0; i < testGrDataGridView.Rows.Count - 1; i++)
            {
                int a = Convert.ToInt32(testGrDataGridView.Rows[i].Cells[5].Value);
                
                OracleCommand cmd = new OracleCommand("select * from VER3.RARITY where ARTWORK_ID='" + a+"'", con);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    testGrDataGridView.Rows[i].Cells[6].Value = true;
                }
                else
                {
                    testGrDataGridView.Rows[i].Cells[6].Value = false;

                }
                
            }
            con.Close();
        }
        private void exhibitiontestDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(exhibitiontestDataGridView.SelectedCells[0].Value.ToString()=="EXHIBITION_ID")
            {
                MessageBox.Show(exhibitiontestDataGridView.SelectedCells[0].Value.ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.KeyCode == Keys.Enter)
            {
               
                if (source is BindingSource)
                {
                    dt = (bs.DataSource as DataSet).Tables[bs.DataMember];
                }
                if (source is DataTable)
                {
                   dt = (DataTable)source;
                }

                DataView dv = dt.DefaultView;
              
                dv.RowFilter = string.Format("CITY_NAME like '%{0}%'", textBox1.Text.Trim());
 
                exhibitiontestDataGridView.DataSource = dv.ToTable();
                total();
                

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                exhibitiontestDataGridView.Sort(exhibitiontestDataGridView.Columns[1], ListSortDirection.Ascending);
                total();
            }
            if (comboBox3.SelectedIndex == 1)
            {
                exhibitiontestDataGridView.Sort(exhibitiontestDataGridView.Columns[2], ListSortDirection.Ascending);
                total();
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private string GetSelectedRadioButtonText(GroupBox grb)
        {
            return grb.Controls.OfType<RadioButton>().SingleOrDefault(rad => rad.Checked == true).Text;
        }
        private void mylistener(object sender, EventArgs e)
        {

            if (source is BindingSource)
            {
             
                dt = (bs.DataSource as DataSet).Tables[bs.DataMember];
            }
            if (source is DataTable)
            {
                dt = (DataTable)source;
            }
            DataView dv = dt.DefaultView;
            if (GetSelectedRadioButtonText(groupBox2) == "Art")
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    dv.RowFilter = "DIRECTION = 'Art'";
                    exhibitiontestDataGridView.DataSource = dv.ToTable();
                    total();
                }
                else
                {
                    dv.RowFilter = "DIRECTION = 'Art' AND CITY_NAME='"+textBox1.Text+"'";
                    exhibitiontestDataGridView.DataSource = dv.ToTable();
                    total();
                }
            }
            else
            {

                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    dv.RowFilter = "DIRECTION = 'Sculpture'";
                    exhibitiontestDataGridView.DataSource = dv.ToTable();
                    total();
                }
                else
                {
                    dv.RowFilter = "DIRECTION = 'Sculpture' AND CITY_NAME='" + textBox1.Text + "'";
                    exhibitiontestDataGridView.DataSource = dv.ToTable();
                    total();
                }
            }
            
        }

        private void artistWithArtworkDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void testGrDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss1 = new Form1();
            ss1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        void change()
        {
            string searchValue = aRTWORK_IDTextBox.Text;
            int rowIndex = -1;
            foreach (DataGridViewRow row in testGrDataGridView.Rows)
            {
                if (row.Cells[5].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            if ((bool)testGrDataGridView.Rows[rowIndex].Cells[6].Value)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
        }

        private void aRTWORK_IDTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bindingNavigatorMoveNextItem1_Click(object sender, EventArgs e)
        {
            change();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
    }
}
