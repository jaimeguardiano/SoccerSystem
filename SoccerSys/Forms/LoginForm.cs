using Microsoft.Win32;
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
using Oracle.ManagedDataAccess.Client;
using SoccerSys.Forms;

namespace SoccerSys
{
    public partial class Form1 : Form
    {
        helper _helper = new helper();
       
        public Form1()
        {
            InitializeComponent();
        }

        public void getValueFromRegistry()
        {
            RegistryKey parentKey = Registry.LocalMachine;
            RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true);
            RegistryKey subKey = softwareKey.OpenSubKey("SoccerSys", true);


            txtUName.Text = _helper.DecryptString(subKey.GetValue("uName").ToString());
            txtPass.Text = _helper.DecryptString(subKey.GetValue("uPass").ToString());
            txtIpAdd.Text = _helper.DecryptString(subKey.GetValue("ip_add").ToString());
            txtDBName.Text = _helper.DecryptString(subKey.GetValue("ServiceName").ToString());

            subKey.Close();
            softwareKey.Close();
            parentKey.Close();
        }
        private void iconButton4_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            RegistryKey parentKey = Registry.LocalMachine;
            RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true);
            RegistryKey subKey = softwareKey.OpenSubKey("SoccerSys", true);

            //storing the values  
            subKey.SetValue("uName", _helper.EncryptString(txtUName.Text));
            subKey.SetValue("uPass", _helper.EncryptString(txtPass.Text));
            subKey.SetValue("ip_add", _helper.EncryptString(txtIpAdd.Text));
            subKey.SetValue("ServiceName", _helper.EncryptString(txtDBName.Text));
            subKey.Close();
            MessageBox.Show("Configuratiton successfully saved.", "SOCCER BOOKING SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel3.Visible = true;
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            this.txtUserName.Clear();
            this.txtUserName.ForeColor = Color.White;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "")
            {
                this.txtUserName.Text = "Username";
                this.txtUserName.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            this.txtPassword.Clear();
            this.txtPassword.ForeColor = Color.White;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "")
            {
                this.txtPassword.Text = "Password";
                this.txtPassword.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //checkUserLogin();

                checkUserLogin();
            }
        }

        //private void checkUser()
        //{
        //    OracleConnection _ocn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));;User id=jaime;Password=jaime123;");
        //    try
        //    {
        //        _ocn.Open();
        //        _ocn.Close();
        //        MessageBox.Show("Connected to server");
        //        _mainForm.Show();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Error connecting to server");
        //    }
        //}

        public void checkUserLogin()
        {
            DataTable _dtMainInfo = new DataTable();
            DataTable _transCharges = new DataTable();

            try
            {
                _dtMainInfo = _helper.loadDataToDataTable("SELECT username,password,fullname FROM SOCCERSYS_USERS WHERE username = '" + txtUserName.Text + "' AND password ='" + txtPassword.Text + "'");
                if (_dtMainInfo.Rows.Count != 0)
                {
                    this.Hide();
                    MainDashboardForm _frm = new MainDashboardForm(_dtMainInfo);
                    _frm.Show();
                }
                else
                {
                    MessageBox.Show("User not exist. Please contact administrator.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logging in encountered an error. Error message: " + ex.Message.ToString());
            }
            //this.Hide();
            //Form1 _frm = new Form1("");
            //_frm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
