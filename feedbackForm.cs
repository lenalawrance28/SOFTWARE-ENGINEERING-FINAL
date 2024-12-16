using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Software_Engineering1
{
    public partial class feedbackForm : Form
    {
        private string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";
        public feedbackForm()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text.Trim();
            string feedbackText = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(feedbackText))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO feedback (Name, FeedbackText) VALUES (@Username, @FeedbackText);";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@FeedbackText", feedbackText);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Feedback submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();
                            textBox2.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit feedback. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
