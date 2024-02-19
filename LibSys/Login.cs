using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibSys
{
    public partial class frmLogin : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= LibSys.mdb";
        private OleDbConnection con;
        public frmLogin()
        {
            InitializeComponent();
            con = new OleDbConnection(connectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            string username = txtBoxUser.Text;
            string password = txtBoxPass.Text;
           
            OleDbCommand cmd = new OleDbCommand("SELECT Password FROM Accounts WHERE Username = '" + username + "'", con);
            //OleDbDataReader reader = cmd.ExecuteReader();
            string pass = cmd.ExecuteScalar().ToString();

            if (password == pass)
            {
                frmMain main = new frmMain();
                MessageBox.Show("Success", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                main.ShowDialog();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error", "Login error, wrong password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            con.Close();
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
        }




        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnLogin;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxPass.Clear();
            txtBoxUser.Clear();
        }
    }
}
