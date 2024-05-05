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
using System.Xml.Linq;

namespace Project
{
    public partial class frmStudentEntry : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=HASIB-PC;Initial Catalog=STUDENTSDB;Integrated Security=True;");
        public frmStudentEntry()
        {
            InitializeComponent();
        }
        private void frmStudentEntry_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            con.Open();
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
            con.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPicture.Text=openFileDialog1.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Image
            Image img = Image.FromFile(txtPicture.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms,ImageFormat.Bmp);
            //
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO student (studentId, studentname, DateOfBirth, roundsid, courseid, TSPid, picture) VALUES(@i, @n, @d, @r, @c, @t, @p)";
            cmd.Parameters.AddWithValue("@i", txtStudentId.Text);
            cmd.Parameters.AddWithValue("@n", txtStudentName.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@r", cmbRoundsId.SelectedValue);
            cmd.Parameters.AddWithValue("@c", cmbCourseId.SelectedValue);
            cmd.Parameters.AddWithValue("@t", cmbTSPId.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value= ms.ToArray() });
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Saved Successfully...!!!!";
            con.Close();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtStudentName.Clear();
            txtPicture.Clear();
            cmbRoundsId.SelectedIndex = -1;
            cmbCourseId.SelectedIndex = -1;
            dateTimePicker1.Text = "";
            cmbTSPId.SelectedIndex = -1;
        }
    }
}
