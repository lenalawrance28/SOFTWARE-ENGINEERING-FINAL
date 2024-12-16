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
    public partial class AdminForm : Form
    {
        string connectionString;

        // Constructor that initializes the AdminForm with a given database connection string.
        public AdminForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadUserDetails(); // Loads all user details into the grid view.
        }

        // Loads user details into a DataGridView, with an optional search query to filter results.
        private void LoadUserDetails(string searchQuery = "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query to fetch user details. If a search query is provided, filter the results.
                    string query = @"SELECT Username, MembershipType, Role, PredominantInterest 
                             FROM users";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += @" WHERE Username LIKE @searchQuery 
                            ORDER BY CASE 
                                WHEN Username LIKE @exactSearch THEN 1 
                                ELSE 2 
                              END, Username";
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add search parameters if a search query is provided.
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            command.Parameters.AddWithValue("@searchQuery", $"%{searchQuery}%");
                            command.Parameters.AddWithValue("@exactSearch", searchQuery);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Display the results in a DataGridView.
                            dataGridView2.DataSource = dataTable;
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message if user details cannot be loaded.
                    MessageBox.Show($"An error occurred while loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Initializes the autocomplete feature for the username search textbox.
        private void InitializeAutoComplete()
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Username FROM users";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Add each username to the autocomplete source.
                        while (reader.Read())
                        {
                            autoComplete.Add(reader.GetString("Username"));
                        }
                    }

                    // Configure the autocomplete settings for the textbox.
                    textBox1.AutoCompleteCustomSource = autoComplete;
                    textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                catch (Exception ex)
                {
                    // Display an error message if autocomplete setup fails.
                    MessageBox.Show($"An error occurred while setting up autocomplete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Loads all user bookings and displays them in a DataGridView.
        private void LoadUserBookings()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_bookings"; // Query to fetch all user bookings.
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable bookingsTable = new DataTable();

                    // Fill the bookings table and bind it to the DataGridView.
                    adapter.Fill(bookingsTable);
                    dataGridView1.DataSource = bookingsTable;
                }
            }
            catch (Exception ex)
            {
                // Display an error message if booking data cannot be loaded.
                MessageBox.Show("Error loading user bookings: " + ex.Message);
            }
        }

        // Event handler for form load event to initialize the form data.
        private void Form4_Load(object sender, EventArgs e)
        {
            LoadPendingApprovals(); // Load pending user approvals into the grid.
            LoadUserBookings(); // Load user bookings into the grid.
            InitializeAutoComplete(); // Set up autocomplete for username search.
        }

        // Loads users pending approval into a DataGridView for admin review.
        private void LoadPendingApprovals()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Username FROM Users WHERE IsApproved = FALSE";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the results to a DataGridView and hide the UserID column.
                    dgvPendingApprovals.DataSource = dataTable;
                    dgvPendingApprovals.Columns["UserID"].Visible = false; // Hide UserID column
                    dgvPendingApprovals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    // Display an error message if pending approvals cannot be loaded.
                    MessageBox.Show($"Error loading pending approvals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Approves the selected user and updates their status in the database.
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvPendingApprovals.SelectedRows.Count > 0)
            {
                // Get the selected user's ID and username from the grid.
                int userId = Convert.ToInt32(dgvPendingApprovals.SelectedRows[0].Cells["UserID"].Value);
                string username = dgvPendingApprovals.SelectedRows[0].Cells["Username"].Value.ToString();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"
                                        UPDATE Users
                                        SET IsApproved = TRUE
                                        WHERE UserID = @userId";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", userId);

                        // Execute the query and check if the operation was successful.
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"User '{username}' has been approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPendingApprovals(); // Refresh the pending approvals list.
                        }
                        else
                        {
                            MessageBox.Show("Approval failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display an error message if approval fails.
                        MessageBox.Show($"Error approving user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Notify the admin to select a user for approval.
                MessageBox.Show("Please select a user to approve.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Navigates to the dashboard form and hides the current form.
        private void button2_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Hide();
        }

        // Updates the user list based on the search query entered in the textbox.
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            LoadUserDetails(searchQuery); // Filter the user list using the search query.
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
    }
}
