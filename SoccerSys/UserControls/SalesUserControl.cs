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
    public partial class SalesUserControl : UserControl
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();
        int curRow = -1;
        public SalesUserControl()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
              SalesForm _frm = new SalesForm("");
            _frm.ShowDialog(this);
        }

        public void loadDatatoGridView()
        {
            _clearDT = new DataTable();
            dgSales.DataSource = _clearDT;

            _dt = _helper.loadDataToDataTable("SELECT S.SALEID, S.TICKETNUMBER AS \"TICKET NUMBER\", S.NUM_SEATS AS \"NO.OF SEATS\",C.DESCRIPTION AS \"CATEGORY\",F.MATCHNAME AS \"MATCH NAME\", S.PRICE AS \"PRICE\" " +
                                                      ", S.PRICE * S.NUM_SEATS AS \"TOTAL AMOUNT\" FROM SALE S LEFT JOIN CATEGORY C ON S.CATEGORYID = C.CATCODE LEFT JOIN FIXTURE F ON S.MATCHID = F.MATCHID");
            dgSales.DataSource = _dt;

            dgSales.Columns[0].Visible = false;

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            loadDatatoGridView();
            try
            {
                if (dgSales.Rows.Count != 0)
                {
                    //dgPurchaseTransaction.Rows[0].Selected = true;
                    dgSales.CurrentCell = dgSales.Rows[0].Cells[1];
                }
            }
            catch
            {
            }
        }

        private void dgSales_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgSales.Rows.Count != 0)
                {
                    curRow = dgSales.CurrentCell.RowIndex;
                }
            }
            catch
            {
                curRow = 0;

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (curRow != -1)
            {
                SalesForm _frm = new SalesForm(dgSales.Rows[curRow].Cells[0].Value.ToString());
                _frm.ShowDialog(this);
            }
        }
    }
}
