using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class WORK : Form
    {
        public WORK()
        {
            InitializeComponent();
        }

        private void courseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcourseentry fce = new frmcourseentry();
            fce.Show();
            fce.MdiParent = this;
        }

        private void roundsEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoundsEntry fre = new frmRoundsEntry();
            fre.Show();
            fre.MdiParent = this;
        }

        private void tspEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTSPEntry fte = new frmTSPEntry();
            fte.Show();
            fte.MdiParent = this;
        }

        private void studentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentEntry fse = new frmStudentEntry();
            fse.Show();
            fse.MdiParent = this;
        }

        private void updatedeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcourseUpdateDelete fcud = new frmcourseUpdateDelete();
            fcud.Show();
            fcud.MdiParent = this;
        }

        private void updatedeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRoundsUpdateDelete frud = new frmRoundsUpdateDelete();
            frud.Show();
            frud.MdiParent = this;
        }

        private void updatedeleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmTspUpdateDelete ftud = new frmTspUpdateDelete();
            ftud.Show();
            ftud.MdiParent = this;
        }

        private void updatedeleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmStudentUpdateDelete fsud = new frmStudentUpdateDelete();
            fsud.Show();
            fsud.MdiParent = this;
        }

        private void stuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentInformationReport fsir = new frmStudentInformationReport();
            fsir.Show();
            fsir.MdiParent = this;
        }
    }
}
