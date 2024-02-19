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
    public partial class Borrow : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= LibSys.mdb";
        private OleDbConnection con;
        private OleDbCommand command;
        private int borrowedAccession = 0;
        public Borrow()
        {
            InitializeComponent();
            con = new OleDbConnection(connectionString);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();

            OleDbCommand com = new OleDbCommand("Select * from book where title like '%" + txtSearch.Text + "%'", con);
            //com.Parameters.AddWithValue("@title", txttitle.Text); 
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            con.Close();
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grid1.SelectedCells.Count > 0)
            {
                int selectedrowindex = grid1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = grid1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["accession_number"].Value);
                borrowedAccession = int.Parse(cellValue);
                string title = Convert.ToString(selectedRow.Cells["title"].Value);
                labelBorrowedBook.Text = "Book to borrow: " + title;
            }
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            
            if(borrowedAccession == 0)
            {
                MessageBox.Show("Click book to borrow in the grid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            con.Open();

            try
            {
                command = new OleDbCommand("SELECT status FROM book WHERE accession_number = " + borrowedAccession, con);
                string status = command.ExecuteScalar().ToString();
                
                if (status == "Not Available")
                {
                    MessageBox.Show("Book not available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return;
                }

                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                int age = int.Parse(txtAge.Text);
                int daysBorrow = int.Parse(txtDaysBorrow.Text);

                command = new OleDbCommand("UPDATE book SET status = 'Not Available' WHERE accession_number = " + borrowedAccession, con);
                command.ExecuteNonQuery();

                command = new OleDbCommand("SELECT MAX(BorrowerID) FROM Borrower", con);
                int borID = Convert.ToInt32(command.ExecuteScalar());

                int newBorId = borID + 1;

                command = new OleDbCommand("INSERT INTO borrower VALUES(borId, FirstName, LastName, Age, DaysBorrow, accNo)", con);
                command.Parameters.AddWithValue("@borId", newBorId);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@DaysBorrow", daysBorrow);
                command.Parameters.AddWithValue("@accNo", borrowedAccession);
                command.ExecuteNonQuery();



                MessageBox.Show("Successfully Borrowed!", "Borrowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Input the right format or input all in the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close(); 
            }
            

        }
    }
}
