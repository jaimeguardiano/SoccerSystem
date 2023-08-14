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
    public partial class TeamsForm : Form
    {
        helper _helper = new helper();
        int curRow = 0;

        string _teamID;
        public TeamsForm(string teamID)
        {
            InitializeComponent();
            _teamID = teamID;

            if (_teamID != "")
            {
                loadDataFromDB();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Are you sure you want to save this TEAM?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {

                string _response = _helper.executeSingleQury("INSERT INTO TEAMS(TEAMNAME) VALUES('" + txtTeamName.Text + "')");

                if (_response == "")
                {
                    MessageBox.Show("Team successfully added to database!");
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
            txtTeamName.Clear();
            txtTeamID.Text = "0";
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult rslt = MessageBox.Show("Are you sure you want to save this team?", "SOCCER BOOKING SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rslt == DialogResult.Yes)
            {


                string _response = _helper.executeSingleQury("UPDATE TEAMS SET teamname = '" + txtTeamName.Text + "' WHERE teamid = " + _teamID);

                if (_response == "")
                {
                    MessageBox.Show("Team updated to database!");
                    resetFields();
                }
                else
                {
                    MessageBox.Show("Updating encountered an error: " + _response);
                }

            }
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
                _dtMainInfo = _helper.loadDataToDataTable("SELECT * FROM teams where TEAMID = " + _teamID);

                txtTeamID.Text = _dtMainInfo.Rows[0][0].ToString();
                txtTeamName.Text = _dtMainInfo.Rows[0][1].ToString();
               

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
