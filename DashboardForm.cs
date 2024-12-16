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
    public partial class DashboardForm : Form
    {
        // Connection string for the database.
        private string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";

        // Constructor to initialize the form and display member count.
        public DashboardForm()
        {
            InitializeComponent();
            DisplayMemberCount(); // Display the total number of members.
        }

        // Event handler for navigating to the EventForm.
        private void button1_Click(object sender, EventArgs e)
        {
            EventForm form1 = new EventForm();
            form1.Show();
            this.Hide(); // Hide the current form.
        }

        // Displays the total count of members in the system on the dashboard.
        private void DisplayMemberCount()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users;"; // Query to count the number of users.

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Execute the query and convert the result to an integer.
                        int memberCount = Convert.ToInt32(command.ExecuteScalar());
                        label4.Text = $"{memberCount}"; // Update the label with the count.
                    }
                }
                catch (Exception ex)
                {
                    // Show an error message if the member count could not be retrieved.
                    MessageBox.Show($"Error retrieving member count: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Static class to manage the currently logged-in user's details.
        public static class LoggedInUser
        {
            private static string _username = "Guest"; // Default username is "Guest".
            private static bool _isLoggedIn = false;  // Default login status is false.

            // Property to get or set the username.
            public static string Username
            {
                get => _username;
                set => _username = value;
            }

            // Property to get or set the login status.
            public static bool IsLoggedIn
            {
                get => _isLoggedIn;
                set => _isLoggedIn = value;
            }
        }

        // Event handler for the logout label click.
        private void label2_Click(object sender, EventArgs e)
        {
            // Reset the logged-in user's details to default (Guest).
            LoggedInUser.Username = "Guest";
            LoggedInUser.IsLoggedIn = false;

            // Redirect to the LoginForm.
            LoginForm form5 = new LoginForm();
            this.Hide();
            form5.Show();
        }

        // Event handler for the form load event to initialize user-specific settings.
        private void Form6_Load(object sender, EventArgs e)
        {
            label2.Text = $"{LoggedInUser.Username}"; // Display the logged-in user's username.
            label8.Visible = LoggedInUser.IsLoggedIn; // Show or hide label based on login status.
        }

        // Event handler to open the feedback form.
        private void button7_Click(object sender, EventArgs e)
        {
            feedbackForm feedbackForm = new feedbackForm();
            feedbackForm.Show();
        }

        // Event handler to navigate to the EventForm.
        private void button6_Click_1(object sender, EventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.Show();
            this.Hide(); // Hide the current form.
        }

        // Event handler to navigate to the membership form.
        private void button3_Click(object sender, EventArgs e)
        {
            membershipForm membershipForm = new membershipForm();
            membershipForm.Show();
            this.Hide(); // Hide the current form.
        }

        // Event handler to open the membership form without hiding the dashboard.
        private void button5_Click(object sender, EventArgs e)
        {
            membershipForm membershipForm = new membershipForm();
            membershipForm.Show();
        }

        // Event handler to open the profile page for the logged-in user.
        private void label8_Click(object sender, EventArgs e)
        {
            string currentUsername = LoggedInUser.Username; // Get the current username.
            ProfilePage profilePage = new ProfilePage(currentUsername); // Pass username to ProfilePage.
            profilePage.Show();
        }

        // Event handler to navigate to the shop form.
        private void button4_Click(object sender, EventArgs e)
        {
            ShopForm sform = new ShopForm();
            sform.Show();
            this.Hide(); // Hide the current form.
        }

        // Placeholder for handling click events on PictureBox (currently not implemented).
        private void pictureBox5_Click(object sender, EventArgs e)
        {
        }
    }
}
