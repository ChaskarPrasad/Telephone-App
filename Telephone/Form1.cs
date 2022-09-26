using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace Telephone
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(
                        "Data Source=APH10027\\SQLEXPRESS;" +
                        "Initial Catalog=Database_Name;" +
                        "User ID=Your User Id;Password=Your Passwaord");
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand(@"INSERT INTO Contact(Name,Mobile,Email,Category)
            values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+comboBox1.Text+"')",conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Contact Saved...");
            Display();
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * from Contact",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Name"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Mobile"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Email"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Category"].ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           textBox1.Text =  dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
           textBox2.Text =  dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           textBox3.Text =  dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
           comboBox1.Text =  dataGridView1.SelectedRows[0].Cells[3].Value.ToString() ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand(@"DELETE FROM Contact 
                                                  WHERE (Mobile='"+textBox2.Text+"')", 
                                                conn
                                                );
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Contact Deleted...");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            Display();
        }
    }
}
