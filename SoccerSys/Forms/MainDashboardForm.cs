using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerSys.Forms
{
    public partial class MainDashboardForm : Form
    {
        DataTable _dtUser = new DataTable();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public MainDashboardForm(DataTable _dtUserInfo)
        {
            InitializeComponent();
            _dtUser = _dtUserInfo;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            iconButton2.BackColor = Color.FromArgb(28, 28, 28);
            iconButton1.BackColor = Color.FromArgb(58, 58, 58);
            iconButton3.BackColor = Color.FromArgb(58, 58, 58);
            iconButton4.BackColor = Color.FromArgb(58, 58, 58);
            iconButton5.BackColor = Color.FromArgb(58, 58, 58);
            iconButton7.BackColor = Color.FromArgb(58, 58, 58);

            categoryUserControl1.Visible = true;
            teamUserControl1.Visible = false;
            fixtureUserControl1.Visible = false;
            salesUserControl1.Visible = false;
        }

        private void MainDashboardForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

            this.Dispose();
            Form1 _frm = new Form1();
            _frm.Show();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            iconButton2.BackColor = Color.FromArgb(58, 58, 58);
            iconButton1.BackColor = Color.FromArgb(58, 58, 58);
            iconButton3.BackColor = Color.FromArgb(58, 58, 58);
            iconButton4.BackColor = Color.FromArgb(58, 58, 58);
            iconButton5.BackColor = Color.FromArgb(58, 58, 58);
            iconButton7.BackColor = Color.FromArgb(28, 28, 28);

            categoryUserControl1.Visible = false;
            teamUserControl1.Visible = true;
            fixtureUserControl1.Visible = false;
            salesUserControl1.Visible = false;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            iconButton2.BackColor = Color.FromArgb(58, 58, 58);
            iconButton1.BackColor = Color.FromArgb(58, 58, 58);
            iconButton3.BackColor = Color.FromArgb(58, 58, 58);
            iconButton4.BackColor = Color.FromArgb(28, 28, 28);
            iconButton5.BackColor = Color.FromArgb(58, 58, 58);
            iconButton7.BackColor = Color.FromArgb(58, 58, 58);

            categoryUserControl1.Visible = false;
            teamUserControl1.Visible = false;
            fixtureUserControl1.Visible = true;
            salesUserControl1.Visible = false;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            iconButton2.BackColor = Color.FromArgb(58, 58, 58);
            iconButton1.BackColor = Color.FromArgb(58, 58, 58);
            iconButton4.BackColor = Color.FromArgb(58, 58, 58);
            iconButton3.BackColor = Color.FromArgb(28, 28, 28);
            iconButton5.BackColor = Color.FromArgb(58, 58, 58);
            iconButton7.BackColor = Color.FromArgb(58, 58, 58);

            categoryUserControl1.Visible = false;
            teamUserControl1.Visible = false;
            fixtureUserControl1.Visible = false;
            salesUserControl1.Visible = true;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            iconButton2.BackColor = Color.FromArgb(58, 58, 58);
            iconButton3.BackColor = Color.FromArgb(58, 58, 58);
            iconButton4.BackColor = Color.FromArgb(58, 58, 58);
            iconButton1.BackColor = Color.FromArgb(58, 58, 58);
            iconButton5.BackColor = Color.FromArgb(58, 58, 58);
            iconButton7.BackColor = Color.FromArgb(58, 58, 58);

            categoryUserControl1.Visible = false;
            teamUserControl1.Visible = false;
            fixtureUserControl1.Visible = false;
            salesUserControl1.Visible = false;
        }

        private void MainDashboardForm_Load(object sender, EventArgs e)
        {
            lblGreetings.Text = "Hi, you are logged in as " + _dtUser.Rows[0][2].ToString();
        }
    }
}
