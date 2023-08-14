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
    public partial class FixtureForm : Form
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();

        string _fixtureID;
        public FixtureForm(string fixtureID)
        {
            InitializeComponent();
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            loadDataToCombobox();

            _fixtureID = fixtureID;
            if (_fixtureID != "")
            {
                loadDataFromDB();
            }

        }

        private void FixtureForm_Load(object sender, EventArgs e)
        {

        }

        public void loadDataToCombobox()
        {
            DataTable _dtTeams1 = new DataTable();
            DataTable _dtTeams2 = new DataTable();

            try
            {
                _dtTeams1 = _helper.loadDataToDataTable("SELECT teamid,teamname FROM teams");
                _dtTeams2 = _helper.loadDataToDataTable("SELECT teamid,teamname FROM teams");

                comboBox1.DisplayMember = "teamname";
                comboBox1.ValueMember = "teamid";
                comboBox1.DataSource = _dtTeams1;

                comboBox2.DisplayMember = "teamname";
                comboBox2.ValueMember = "teamid";
                comboBox2.DataSource = _dtTeams2;

                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading data encountered an error. Error message: " + ex.Message.ToString());
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Are you sure you want to update this match?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {


                string _response = _helper.executeSingleQury("UPDATE fixture SET MATCH_DATE = '" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "',MATCHTIME = '" + dateTimePicker2.Text + "'," +
                                                                "TEAMID1 = " + comboBox1.SelectedValue + ",TEAMID2 = " + comboBox2.SelectedValue + ",MATCHNAME = '" + txtMatchName.Text + "' WHERE matchid = " + _fixtureID);
                    
                if (_response == "")
                {
                    MessageBox.Show("Match updated to database!");
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

            DialogResult rslt = MessageBox.Show("Are you sure you want to save this match?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {

                string _response = _helper.executeSingleQury("INSERT INTO fixture(MATCH_DATE,MATCHTIME,TEAMID1,TEAMID2, MATCHNAME) VALUES('" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "', " +
                                                                " '" + dateTimePicker2.Text + "', " + comboBox1.SelectedValue + "," + comboBox2.SelectedValue + ", '"+ txtMatchName.Text +"')");

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
            txtMatchName.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        public void loadDataFromDB()
        {
            DataTable _dtMainInfo = new DataTable();

            try
            {
                _dtMainInfo = _helper.loadDataToDataTable("SELECT * FROM FIXTURE where matchid = " + _fixtureID);

                txtMatchID.Text = _dtMainInfo.Rows[0][0].ToString();
                dateTimePicker1.Text = _dtMainInfo.Rows[0][1].ToString();
                dateTimePicker2.Text = _dtMainInfo.Rows[0][2].ToString();
                comboBox1.SelectedValue = int.Parse(_dtMainInfo.Rows[0][5].ToString());
                comboBox2.SelectedValue = int.Parse(_dtMainInfo.Rows[0][6].ToString());
                txtMatchName.Text = _dtMainInfo.Rows[0][7].ToString();

                toolStripButton1.Visible = false;
                toolStripButton2.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading data encountered an error. Error message: " + ex.Message.ToString());
            }
        }

      
    }
}
