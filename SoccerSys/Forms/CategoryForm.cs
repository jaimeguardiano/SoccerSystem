using SoccerSys.Helper;
using SoccerSys.UserControls;
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
    public partial class CategoryForm : Form
    {
        helper _helper = new helper();
        int curRow = 0;

        string _catId;
        public CategoryForm(string catID)
        {
            InitializeComponent();
            _catId = catID;

            if (_catId != "")
            {
                loadDataFromDB();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Are you sure you want to save this category?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {

                string _response = _helper.executeSingleQury("INSERT INTO category(description,noseats,price,seatfrom,seatto,status) VALUES('"+ txtDescription.Text +"', " + txtSeats.Text+ ", " + txtPrice.Text + "," + txtSeatsFrom.Text + "," + txtSeatsTo.Text + ", '')");

                if(_response == "")
                {
                    MessageBox.Show("Category successfully added to database!");
                    resetFields();
                }
                else
                {
                    MessageBox.Show("Saving encountered an error: " + _response);
                }

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        public void resetFields()
        {
            txtDescription.Clear();
            txtPrice.Clear();
            txtSeats.Clear();
            txtSeatsFrom.Clear();
            txtSeatsTo.Clear();
            txtCatID.Text = "0";
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = false;
        }

        public void loadDataFromDB()
        {
            DataTable _dtMainInfo = new DataTable();

            try
            {
                _dtMainInfo = _helper.loadDataToDataTable("SELECT * FROM CATEGORY where CATCODE = " + _catId);

                txtCatID.Text = _dtMainInfo.Rows[0][0].ToString();
                txtDescription.Text = _dtMainInfo.Rows[0][1].ToString();
                txtSeats.Text = _dtMainInfo.Rows[0][2].ToString();
                txtPrice.Text = _dtMainInfo.Rows[0][3].ToString();
                txtSeatsFrom.Text = _dtMainInfo.Rows[0][4].ToString();
                txtSeatsTo.Text = _dtMainInfo.Rows[0][5].ToString();

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
            DialogResult rslt = MessageBox.Show("Are you sure you want to save this category?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {

            
                string _response = _helper.executeSingleQury("UPDATE category SET description = '" + txtDescription.Text + "',noseats = " + txtSeats.Text + ",price = " + txtPrice.Text + ",seatfrom = " + txtSeatsFrom.Text + ",seatto = " + txtSeatsTo.Text + " WHERE CATCODE = " + _catId);

                if (_response == "")
                {
                    MessageBox.Show("Category updated to database!");
                    resetFields();
                }
                else
                {
                    MessageBox.Show("Updating encountered an error: " + _response);
                }

            }
        }
    }
}
