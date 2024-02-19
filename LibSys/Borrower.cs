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
    public partial class Borrower : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= LibSys.mdb";
        private OleDbConnection con;

        public Borrower()
        {
            InitializeComponent();
            con = new OleDbConnection(connectionString);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                //OleDbCommand com = new OleDbCommand("Insert into Borrower VALUES (" + int.Parse(txtID.Text) + ",'"+ txtFirstName.Text + "'," + txtLastName.Text + "'," + int.Parse(txtAge.Text) + "," + int.Parse(txtLastName.Text) + ");", con);
                OleDbCommand com = new OleDbCommand("Insert into Borrower (BorrowerID, FirstName, LastName, Age, DaysBorrow) VALUES (?, ?, ? , ?, ?);", con);

                com.Parameters.AddWithValue("@BorrowerID", int.Parse(txtID.Text));
                com.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                com.Parameters.AddWithValue("@LastName", txtLastName.Text);
                com.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                com.Parameters.AddWithValue("@DaysBorrow", int.Parse(txtDaysBorrow.Text));
                com.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                MessageBox.Show("Error!", "Input all information, and approprtiately.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }


            MessageBox.Show("Successfully SAVED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            con.Open();

            OleDbCommand com = new OleDbCommand("Select * from Borrower order by BorrowerID asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            con.Close();
        }

        private void Borrower_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                int num = int.Parse(txtID.Text);

                DialogResult dr;

                dr = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    OleDbCommand com = new OleDbCommand("Delete from Borrower where BorrowerID = " + num, con);
                    //com.Parameters.AddWithValue("@accesion_number", txtno.Text);
                    com.ExecuteNonQuery();

                    MessageBox.Show("Successfully DELETED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Successfully CANCELLED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
                LoadDataGrid();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter borrower ID number to delete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int no = int.Parse(txtID.Text);
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                int age = int.Parse(txtAge.Text);
                int daysBorrow = int.Parse(txtDaysBorrow.Text);

                OleDbCommand com = new OleDbCommand("Update Borrower SET FirstName = '" + firstName + "', LastName='" + lastName + "', Age = " + age +  ", DaysBorrow = "+ daysBorrow + " where BorrowerID = " + no, con);

                if(com.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Error! Information not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return;
                }

                MessageBox.Show("Successfully UPDATED!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                LoadDataGrid();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error! Enter all information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int no = int.Parse(txtID.Text);
                OleDbCommand com = new OleDbCommand("SELECT * FROM Borrower WHERE BorrowerID = " + no, con);
                com.ExecuteNonQuery();
                OleDbDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("Successfully shown!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                reader.Close();
                OleDbDataAdapter adap = new OleDbDataAdapter(com);
                DataTable tab = new DataTable();

                adap.Fill(tab);
                grid1.DataSource = tab;


                con.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error! Enter number for Borrower ID Number only!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
