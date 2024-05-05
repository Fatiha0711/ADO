using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project
{
    public partial class frmRoundsEntry : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HASIB-PC;Initial Catalog=STUDENTSDB;Integrated Security=True;");
        public frmRoundsEntry()
        {
            InitializeComponent();
        }
      

        private void frmRoundsEntry_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM rounds", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO rounds VALUES('" + txtRoundsName.Text + "')", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Saved Successfully...!!!";
            LoadGrid();
            txtRoundsName.Text = "";
            con.Close();
        }
    }
}
