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
    public partial class CategoryUserControl : UserControl
    {
        helper _helper = new helper();
        DataTable _dt = new DataTable();
        DataTable _clearDT = new DataTable();
        int curRow = -1;

        public CategoryUserControl()
        {
            InitializeComponent();
          
        }

        public void loadDatatoGridView()
        {
            _clearDT = new DataTable();
            dgCategory.DataSource = _clearDT;

            _dt = _helper.loadDataToDataTable("SELECT CATCODE AS \"CATEGORY CODE\", DESCRIPTION, NOSEATS AS \"SEATS NUMBER\", PRICE, SEATFROM AS \"SEATS FROM\", SEATTO AS \"SEATS TO\", STATUS FROM category");
            dgCategory.DataSource = _dt;

            dgCategory.Columns[0].Visible = false;
            this.dgCategory.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            loadDatatoGridView();
            try
            {
                if (dgCategory.Rows.Count != 0)
                {
                    //dgPurchaseTransaction.Rows[0].Selected = true;
                    dgCategory.CurrentCell = dgCategory.Rows[0].Cells[1];
                }
            }
            catch
            {
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
         
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CategoryForm _frm = new CategoryForm("");
            _frm.ShowDialog(this);
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (curRow != -1)
            {
                CategoryForm _frm = new CategoryForm(dgCategory.Rows[curRow].Cells[0].Value.ToString());
                _frm.ShowDialog(this);
            }
           
        }

        private void dgCategory_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgCategory.Rows.Count != 0)
                {
                    curRow = dgCategory.CurrentCell.RowIndex;
                }
            }
            catch
            {
                curRow = 0;

            }
        }
    }
}
