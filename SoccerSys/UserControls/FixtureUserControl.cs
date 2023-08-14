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
    public partial class FixtureUserControl : UserControl
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();
        int curRow = -1;

        public FixtureUserControl()
        {
            InitializeComponent();
        }

        public void loadDatatoGridView()
        {
            _clearDT = new DataTable();
            dgFixture.DataSource = _clearDT;

            _dt = _helper.loadDataToDataTable("SELECT F.MATCHID,F.MATCHNAME AS \"MATCH NAME\",F.MATCH_DATE as \"MATCH DATE\", F.MATCHTIME AS \"MATCH TIME\", F.TICKETSOLD AS \"SOLD TICKETS\", T1.TEAMNAME AS \"FIRST TEAM\", T2.TEAMNAME AS \"SECOND TEAM\" FROM FIXTURE F" +
                                                " LEFT JOIN teams T1 ON f.teamid1 = T1.TEAMID LEFT JOIN teams T2 ON f.teamid2 = t2.teamid");
            dgFixture.DataSource = _dt;

            dgFixture.Columns[0].Visible = false;

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            loadDatatoGridView();
            try
            {
                if (dgFixture.Rows.Count != 0)
                {
                    //dgPurchaseTransaction.Rows[0].Selected = true;
                    dgFixture.CurrentCell = dgFixture.Rows[0].Cells[1];
                }
            }
            catch
            {
            }
        }

        private void dgFixture_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgFixture.Rows.Count != 0)
                {
                    curRow = dgFixture.CurrentCell.RowIndex;
                }
            }
            catch
            {
                curRow = 0;

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FixtureForm _frm = new FixtureForm("");
            _frm.ShowDialog(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (curRow != -1)
            {
                FixtureForm _frm = new FixtureForm(dgFixture.Rows[curRow].Cells[0].Value.ToString());
                _frm.ShowDialog(this);
            }
        }
    }
}
