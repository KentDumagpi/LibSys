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
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LibSys
{
    public partial class RegisterForm : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= LibSys.mdb";
        private OleDbConnection con;
        
        public RegisterForm()
        {
            InitializeComponent();
            con = new OleDbConnection(connectionString);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                string username = txtBoxUser.Text;
                string password = txtBoxPass.Text;

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Accounts VALUES(@username, @pass);", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Registered!", "You have succesfully registered, " + username, MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                con.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error", "Input username and password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnRegister;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxPass.Clear();
            txtBoxUser.Clear();
        }
    }
}
