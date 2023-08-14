using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace SoccerSys.Helper
{

    class helper
    {
        public static string userN, userPass, ip_add, serverString, servname;

        public OracleDataAdapter adpt = new OracleDataAdapter();
        OracleDataReader rd;
        public OracleCommand cmd = new OracleCommand();
        public OracleConnection cnx = new OracleConnection();
        public string connectionstring()
        {
            getValueFromRegistry();

            serverString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+ ip_add +")(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME="+ servname +")));;User id="+ userN +";Password="+ userPass +";";
            // serverString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));;User id=jaime;Password=jaime123;";
            //serverString = "Server=localhost;Database=aimsdb;Uid=root;Pwd=;";

            return serverString;
        }

        public void getValueFromRegistry()
        {
            RegistryKey parentKey = Registry.LocalMachine;
            RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true);
            RegistryKey subKey = softwareKey.OpenSubKey("SoccerSys", true);

            userN = DecryptString(subKey.GetValue("uName").ToString());
            userPass = DecryptString(subKey.GetValue("uPass").ToString());
            ip_add = DecryptString(subKey.GetValue("ip_add").ToString());
            servname = DecryptString(subKey.GetValue("ServiceName").ToString());

            subKey.Close();
            softwareKey.Close();
            parentKey.Close();
        }

        public string DecryptString(string _data)
        {
            string hash = "*1234567890!@#$%^&*()14344*";
            string decrypt = "";
            if (_data == "")
            {
                return "";
            }
            byte[] data = Convert.FromBase64String(_data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }

            return decrypt;
        }


        public string EncryptString(string _data)
        {
            string hash = "*1234567890!@#$%^&*()14344*";
            string encrypt = "";

            byte[] data = UTF8Encoding.UTF8.GetBytes(_data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            return encrypt;
        }

        public DataTable loadDataToDataTable(string qry)
        {
            DataTable _dt = new DataTable();

            cnx.ConnectionString = connectionstring();
            cnx.Open();
            adpt = new OracleDataAdapter(qry, cnx);
            adpt.Fill(_dt);
            cnx.Close();

            return _dt;
        }

        public string executeSingleQury(string qry)
        {
            DataTable _dt = new DataTable();
            string _retValue = "";
            try
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }

                cnx.ConnectionString = connectionstring();

                cnx.Open();
                cmd = new OracleCommand(qry, cnx);
                cmd.ExecuteNonQuery();
                cnx.Close();


                _retValue =  "";
            }
            catch(Exception ex)
            {
                _retValue =  ex.Message.ToString();
            }

            return _retValue;
        }

    }
}
