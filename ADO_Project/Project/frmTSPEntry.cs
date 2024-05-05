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

namespace Project
{
    public partial class frmTSPEntry : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HASIB-PC;Initial Catalog=STUDENTSDB;Integrated Security=True;");
        public frmTSPEntry()
        {
            InitializeComponent();
        }

        private void frmTSPEntry_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO TSP VALUES('" + txtTSPId.Text+"','"+txtTSPName.Text + "')", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Saved Successfully...!!!";
            LoadGrid();
            txtTSPName.Text = "";
            con.Close();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM TSP", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
