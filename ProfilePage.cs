using System;
using System.Windows.Forms;
using MySqlConnector;

namespace Software_Engineering1
{
    public partial class ProfilePage : Form
    {
        private string connectionString = "server=localhost;database=theevents;uid=root;pwd=;"; //connection string for SQL database 
        private string currentUsername; //stores current username 
        public ProfilePage(string currentUsername) //Assigns username and initiazlize 
        {
            InitializeComponent();                  
            this.currentUsername = currentUsername; //Assigns username to private field 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newUsername = textBox1.Text.Trim(); // new username from textBox1
            string predominantInterest = comboBox1.SelectedItem?.ToString(); // New intrest from comboBox1
            DateTime dateOfBirth = dateTimePicker1.Value; //Get date of Birth from date and time picker 

            if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(predominantInterest)) // checks if field is empty 
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString)) //Connection to MySql databse 
            {
                try
                {
                    connection.Open();
                    // SQL query to update user profile 
                    string query = "UPDATE Users SET Username = @newUsername, PredominantInterest = @interest, DateOfBirth = @dob WHERE Username = @currentUsername"; 
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        //Add parameter values to query 
                        command.Parameters.AddWithValue("@newUsername", newUsername);
                        command.Parameters.AddWithValue("@interest", predominantInterest);
                        command.Parameters.AddWithValue("@dob", dateOfBirth);
                        command.Parameters.AddWithValue("@currentUsername", currentUsername);

                        //Execute query to check rows were affected 
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0) 
                        {
                            // if update is sucessfull show pop up sucessfull 

                            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            currentUsername = newUsername; // Update the currentUsername to the new one
                            button2.Enabled = true;
                            button1.Enabled = false;
                            DisableEditingFields();
                        }
                        else // if any rows were affected update failed 
                        {
                            MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) //error catching 
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void ProfilePage_Load(object sender, EventArgs e)
        {
            label4.Text = $"Welcome to your Profile {currentUsername}!"; //welcome messgage with current username 

            // Load user details from database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); //connnection open 
                    string query = "SELECT Username, PredominantInterest, DateOfBirth FROM Users WHERE Username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        //parameterized query to prevent sql injection
                        command.Parameters.AddWithValue("@username", currentUsername);

                        using (MySqlDataReader reader = command.ExecuteReader()) //
                        {
                            if (reader.Read()) //check if result is returned 
                            {
                                //display user details in text box , combo box and date picker
                                textBox1.Text = reader.GetString("Username");
                                comboBox1.SelectedItem = reader["PredominantInterest"].ToString();
                                dateTimePicker1.Value = reader["DateOfBirth"] == DBNull.Value
                                    ? DateTime.Now // if no date and time given use current date and time 
                                    : Convert.ToDateTime(reader["DateOfBirth"]);
                            }
                        }
                    }
                }
                catch (Exception ex) // catch errors
                {
                    MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }   }

        private void button2_Click(object sender, EventArgs e)
        {
            //enable edit fields for user to make changes 
            textBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;

            // Enable the Save Changes button
            button1.Enabled = true;

            // Optionally, disable the Edit button
            button2.Enabled = false;
        }


        private void DisableEditingFields() // disable editing fields 
        {
            textBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
        }

      
    }
}
