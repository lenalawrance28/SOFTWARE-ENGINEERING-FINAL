using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Software_Engineering1
{

    public partial class ForgetpasswordForm : Form
    {
        private string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";

       

        public ForgetpasswordForm()
        {
            InitializeComponent();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim(); // Username
            string oldPassword = textBox2.Text.Trim(); // Old password
            string newPassword = textBox3.Text.Trim(); // New password

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to get the stored password hash for the given username
                    string query = "SELECT PasswordHash FROM users WHERE Username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        object result = command.ExecuteScalar(); // Execute query to get the password hash
                        if (result != null)
                        {
                            string storedPasswordHash = result.ToString(); // Stored hash from the database

                            // Hash the entered old password
                            string enteredOldPasswordHash = HashPassword(oldPassword);
                            MessageBox.Show($"Stored Hash: {storedPasswordHash}\nEntered Hash: {enteredOldPasswordHash}");
                            if (storedPasswordHash == enteredOldPasswordHash) // Compare hashes
                            {
                                // Hash the new password before saving it
                                string newPasswordHash = HashPassword(newPassword);

                                // Update the password with the new hash
                                string updateQuery = "UPDATE users SET PasswordHash = @newPassword WHERE Username = @username";
                                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@newPassword", newPasswordHash);
                                    updateCommand.Parameters.AddWithValue("@username", username);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to update password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("The old password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No account found with the provided username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private string HashPassword(string password)
        {
            // Simple hashing example; use a secure hashing library in production
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password.Trim())); // Trims whitespace
                return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Converts to a hex string
            }
        }

    }
}
