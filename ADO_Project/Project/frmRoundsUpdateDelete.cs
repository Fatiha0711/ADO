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
    public partial class frmRoundsUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HASIB-PC;Initial Catalog=STUDENTSDB;Integrated Security=True;");
        public frmRoundsUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmRoundsUpdateDelete_Load(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT roundsname FROM rounds WHERE roundsid=" + txtRoundsId.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtRoundsName.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                lblMsg.Text = "No Data Found!";
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE rounds set roundsname WHERE roundsid=" + txtRoundsId.Text + "", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Updated Successfully..!!";
            LoadGrid();
            txtRoundsName.Text = "";
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM rounds WHERE roundsid=@i", con);
            cmd.Parameters.AddWithValue("@i", txtRoundsId.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted Successfully..!!";
            con.Close();
        }
    }
}
