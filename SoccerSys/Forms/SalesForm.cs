using SoccerSys.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerSys.Forms
{
    public partial class SalesForm : Form
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();

        string _salesID;
        public SalesForm(string salesID)
        {
            InitializeComponent();

            loadDataToCombobox();

            _salesID = salesID;
            if (_salesID != "")
            {
                loadDataFromDB();
            }
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {

        }

        public void loadDataToCombobox()
        {
            DataTable _dtMatch = new DataTable();
            DataTable _dtCategory = new DataTable();

            try
            {
                _dtMatch = _helper.loadDataToDataTable("SELECT matchid,matchname FROM fixture");
                _dtCategory = _helper.loadDataToDataTable("SELECT catcode,description FROM category");

                comboBox1.DisplayMember = "matchname";
                comboBox1.ValueMember = "matchid";
                comboBox1.DataSource = _dtMatch;

                comboBox2.DisplayMember = "description";
                comboBox2.ValueMember = "CATCODE";
                comboBox2.DataSource = _dtCategory;

                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading data encountered an error. Error message: " + ex.Message.ToString());
            }
        }

        public void loadDataFromDB()
        {
            DataTable _dtMainInfo = new DataTable();

            try
            {
                _dtMainInfo = _helper.loadDataToDataTable("SELECT s.ticketnumber,s.num_seats,s.categoryid,s.matchid,S.price, (S.PRICE*S.NUM_SEATS) FROM sale S where S.saleid = " + _salesID);

              
                txtTicketNum.Text = _dtMainInfo.Rows[0][0].ToString();
                txtNumSeats.Text = _dtMainInfo.Rows[0][1].ToString();
                comboBox2.SelectedValue = int.Parse(_dtMainInfo.Rows[0][2].ToString());
                comboBox1.SelectedValue = int.Parse(_dtMainInfo.Rows[0][3].ToString());
                txtAmount.Text = _dtMainInfo.Rows[0][5].ToString();
                txtPrice.Text = _dtMainInfo.Rows[0][4].ToString();
                

                toolStripButton1.Visible = false;
                toolStripButton2.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading data encountered an error. Error message: " + ex.Message.ToString());
            }
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Are you sure you want to update this transaction?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {


                string _response = _helper.executeSingleQury("UPDATE sale SET NUM_SEATS = " + txtNumSeats.Text + ",CATEGORYID = '" + comboBox2.SelectedValue + "'," +
                                                                "MATCHID = " + comboBox1.SelectedValue + ",PRICE = " + txtPrice.Text + "  WHERE saleid = " + _salesID);

                if (_response == "")
                {
                    MessageBox.Show("Transaction updated to database!");
                    resetFields();
                }
                else
                {
                    MessageBox.Show("Updating encountered an error: " + _response);
                }

            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DialogResult rslt = MessageBox.Show("Are you sure you want to save this transaction?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {

                string _response = _helper.executeSingleQury("INSERT INTO sale(TICKETNUMBER,NUM_SEATS,CATEGORYID,MATCHID, PRICE, SALES_DATE,SALES_TIME) " +
                                                                "VALUES((select to_char((case when max(saleid) is null then 0 else max(saleid) end) + 1,'fm000000') from sale), " +
                                                                 "" + txtNumSeats.Text + ", " + comboBox2.SelectedValue + ", " + comboBox1.SelectedValue + "," + txtPrice.Text + ", " +
                                                                 "'"+ DateTime.Now.ToString("MM-dd-yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "' )");

                if (_response == "")
                {
                    MessageBox.Show("Match successfully added to database!");
                    resetFields();
                }
                else
                {
                    MessageBox.Show("Saving encountered an error: " + _response);
                }

            }
        }

        public void resetFields()
        {
            txtTicketNum.Text = "0";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtAmount.Clear();
            txtNumSeats.Clear();
            txtPrice.Clear();
  
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable _catPrice = new DataTable();
            if (comboBox2.SelectedIndex == -1)
            {
                txtAmount.Clear();
                txtPrice.Clear();
                return;
            }
                
            try
            {
                _catPrice = _helper.loadDataToDataTable("SELECT price FROM category where catcode = " + comboBox2.SelectedValue);
                txtPrice.Text = _catPrice.Rows[0][0].ToString();
                txtAmount.Text = (int.Parse(_catPrice.Rows[0][0].ToString()) * int.Parse(txtNumSeats.Text)).ToString("N2");
            }
            catch(Exception ex)
            {
                
            }
            
        }

    }
}
