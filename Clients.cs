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
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-LT4EDDL6;Initial Catalog=film_productiondb;Integrated Security=True");


        



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            // Load initial data when the form loads
            LoadClients();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string contactNumber = textBox3.Text;
            string email = textBox4.Text;
            string address = textBox5.Text;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Insert data into Clients table
                string insertQuery = "INSERT INTO Clients (first_name, last_name, contact_number, email, address) " +
                                     "VALUES (@firstName, @lastName, @contactNumber, @email, @address);";
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@address", address);

                    cmd.ExecuteNonQuery();
                }

                // Refresh the DataGridView with the updated data
                LoadClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Update the selected row with the new data from text boxes
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming your text boxes are named txtFirstName, txtLastName, txtContactNumber, txtEmail, txtAddress
                string firstName = textBox1.Text;
                string lastName = textBox2.Text;
                string contactNumber = textBox3.Text;
                string email = textBox4.Text;
                string address = textBox5.Text;

                try
                {
                    if(con.State == ConnectionState.Closed)
        {
                        con.Open();
                    }

                    // Update data in Clients table
                    string updateQuery = "UPDATE Clients SET first_name = @firstName, " +
                                         "last_name = @lastName, contact_number = @contactNumber, " +
                                         "email = @email, address = @address " +
                                         "WHERE client_ID = @clientID;";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@contactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@clientID", dataGridView1.SelectedRows[0].Cells["client_ID"].Value);

                        cmd.ExecuteNonQuery();

                    }

                    // Refresh the DataGridView with the updated data
                    LoadClients();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedClientId = (int)dataGridView1.SelectedRows[0].Cells["client_ID"].Value;

                // Perform the deletion
                DeleteClient(selectedClientId);

                // Refresh the DataGridView after deletion
                LoadClients();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadClientData(int clientId)
        {
            // Assuming your text boxes are named textBox1, textBox2, textBox3, textBox4, textBox5
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clients WHERE client_ID = @clientId", con);
            cmd.Parameters.AddWithValue("@clientId", clientId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                textBox1.Text = row["first_name"].ToString();
                textBox2.Text = row["last_name"].ToString();
                textBox3.Text = row["contact_number"].ToString();
                textBox4.Text = row["email"].ToString();
                textBox5.Text = row["address"].ToString();
            }
        }
        private void LoadClients()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clients", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable table = new DataTable();
            da.Fill(table);

            dataGridView1.DataSource = table;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedClientId = (int)dataGridView1.SelectedRows[0].Cells["client_ID"].Value;

                LoadClientData(selectedClientId);
            }
        }
        private void DeleteClient(int clientId)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Assuming "Clients" is your table name
                string deleteQuery = "DELETE FROM Clients WHERE client_ID = @clientId";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Client deleted successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
