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

namespace _5951071040_VoVanKha_DemoWinform
{
    public partial class Form1 : Form
    {
       
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-9DKLIBTR;Initial Catalog=DEMO_CRUD;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }


        public int StudentID;  
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (StudentID > 0)
            {

                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-9DKLIBTR;Initial Catalog=DEMO_CRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name=@Name,FatherName=@FatherName,RollNumber=@RollNumber,Address=@Address,Mobile=@Mobile WHERE StudentID=@ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtTen.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtHo.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSDT.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                

            }
            else
            {
                MessageBox.Show("Cập nhật không thành công!!!", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetData()
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-9DKLIBTR;Initial Catalog=DEMO_CRUD;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("Select * from StudentsTb",con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
                
        }
        private bool IsValidData()
        {
            if(txtHo.Text == String.Empty|| txtTen.Text == String.Empty
                ||txtDiaChi.Text== String.Empty 
                || string.IsNullOrEmpty(txtSDT.Text)
                || string.IsNullOrEmpty(txtSBD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi dữ liệu",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb(Name,FatherName,RollNumber,Address,Mobile) VALUES " +
                    "(@Name,@FatherName,@RollNumber,@Address,@Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtTen.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtHo.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSDT.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.StudentRecordData.Rows[e.RowIndex];
            StudentID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
            txtTen.Text = row.Cells[1].Value.ToString();
            txtHo.Text = row.Cells[2].Value.ToString();
            txtSBD.Text = row.Cells[3].Value.ToString();
            txtDiaChi.Text = row.Cells[4].Value.ToString();
            txtSDT.Text = row.Cells[5].Value.ToString();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
            if (StudentID > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=LAPTOP-9DKLIBTR;Initial Catalog=DEMO_CRUD;Integrated Security=True");
                SqlCommand cmd = new SqlCommand(" delete from StudentsTb where StudentID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();

            }
            else
            {
                MessageBox.Show("Xóa không thành công", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
