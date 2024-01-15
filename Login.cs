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

namespace Quiet_Attic
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=film_productiondb;Integrated Security=True");
        private void Login_Load(object sender, EventArgs e)
        {

        }
        private bool AuthenticateUser(string username, string password)
        {

            con.Open();

            string query = "SELECT COUNT(*) FROM Access WHERE user_name = @username AND password = @password";

            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                con.Close();

                return count > 0;
            }
        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (AuthenticateUser(username, password))
            {

                this.Hide();
                // Successful login
                Dashboard dashboardForm = new Dashboard();
                dashboardForm.Show();

                // Close the login form


                // Open the dashboard form

            }
            else
            {
                // Failed login
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
