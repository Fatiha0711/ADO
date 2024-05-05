using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class frmStudentUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HASIB-PC;Initial Catalog=STUDENTSDB;Integrated Security=True;");
        public frmStudentUpdateDelete()
        {
            InitializeComponent();
        }

        private void LoadCombo()
        {
           
            SqlDataAdapter sdaRounds = new SqlDataAdapter("SELECT * FROM rounds", con);
            DataSet ds = new DataSet();
            sdaRounds.Fill(ds);
            cmbRoundsId.DataSource = ds.Tables[0];
            cmbRoundsId.DisplayMember = "roundsname";
            cmbRoundsId.ValueMember = "roundsid";

            SqlDataAdapter sdaCourse = new SqlDataAdapter("SELECT * FROM course", con);
            DataSet ds2 = new DataSet();
            sdaCourse.Fill(ds2);
            cmbCourseId.DataSource = ds2.Tables[0];
            cmbCourseId.DisplayMember = "coursename";
            cmbCourseId.ValueMember = "courseid";

            SqlDataAdapter sdaTSP = new SqlDataAdapter("SELECT * FROM TSP", con);
            DataSet ds3 = new DataSet();
            sdaTSP.Fill(ds3);
            cmbTSPId.DataSource = ds3.Tables[0];
            cmbTSPId.DisplayMember = "TSPname";
            cmbTSPId.ValueMember = "TSPid";
            
        }

        private void frmStudentUpdateDelete_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT studentId,studentname,DateOfBirth, roundsid, courseid, TSPid, picture FROM student WHERE studentId=" + txtStudentId.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStudentName.Text = dt.Rows[0][1].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][2].ToString());
                cmbRoundsId.SelectedValue = dt.Rows[0][3].ToString();
                cmbCourseId.SelectedValue = dt.Rows[0][4].ToString();
                cmbTSPId.SelectedValue = dt.Rows[0][5].ToString();
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][6]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
            }
            else
            {
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = "No Data Found...!!!";

            }
            con.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPicture.Text = openFileDialog1.FileName;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPicture.Text != "")
            {
                Image img = Image.FromFile(txtPicture.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                //
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE student SET studentname=@n,DateOfBirth=@d,roundsid=@r,courseid=@c,TSPid=@t,picture=@p WHERE studentId=@i";
                cmd.Parameters.AddWithValue("@i", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@n", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@r", cmbRoundsId.SelectedValue);
                cmd.Parameters.AddWithValue("@c", cmbCourseId.SelectedValue);
                cmd.Parameters.AddWithValue("@s", cmbTSPId.SelectedValue);
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
                
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated successfully!!!";
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE student SET studentname=@n,DateOfBirth=@d,roundsid=@r,courseid=@c,TSPid=@t,picture=@p WHERE studentId=@i";
                cmd.Parameters.AddWithValue("@i", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@n", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@r", cmbRoundsId.SelectedValue);
                cmd.Parameters.AddWithValue("@c", cmbCourseId.SelectedValue);
                cmd.Parameters.AddWithValue("@s", cmbTSPId.SelectedValue);
                cmd.ExecuteNonQuery();
                lblMsg.Text = "Data Updated successfully!!!";
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM student WHERE studentId=@i ", con);
            cmd.Parameters.AddWithValue("@i", txtStudentId.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted successfully...!!!";
            con.Close();
        }
    }
}
