using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibSys
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void librarySystremToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LibSys lib = new LibSys();
            lib.ShowDialog();
        }

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrower b = new Borrower();
            b.ShowDialog();
        }

        private void borrowABookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borrow borrow = new Borrow();
            borrow.ShowDialog();
        }
    }
}
