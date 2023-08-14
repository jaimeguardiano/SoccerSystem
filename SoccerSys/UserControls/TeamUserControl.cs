using SoccerSys.Forms;
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

namespace SoccerSys.UserControls
{

    public partial class TeamUserControl : UserControl
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();
        int curRow = -1;
        public TeamUserControl()
        {
            InitializeComponent();
        }

        public void loadDatatoGridView()
        {
            _clearDT = new DataTable();
            dgTeams.DataSource = _clearDT;

            _dt = _helper.loadDataToDataTable("SELECT teamid,TEAMNAME AS \"TEAM NAME\" FROM TEAMS");
            dgTeams.DataSource = _dt;   

            dgTeams.Columns[0].Visible = false;
           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            loadDatatoGridView();
            try
            {
                if (dgTeams.Rows.Count != 0)
                {
                    //dgPurchaseTransaction.Rows[0].Selected = true;
                    dgTeams.CurrentCell = dgTeams.Rows[0].Cells[1];
                }
            }
            catch
            {
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (curRow != -1)
            {
                TeamsForm _frm = new TeamsForm(dgTeams.Rows[curRow].Cells[0].Value.ToString());
                _frm.ShowDialog(this);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TeamsForm _frm = new TeamsForm("");
            _frm.ShowDialog(this);

        }

        private void dgTeams_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgTeams.Rows.Count != 0)
                {
                    curRow = dgTeams.CurrentCell.RowIndex;
                }
            }
            catch
            {
                curRow = 0;

            }
        }
    }
}       
