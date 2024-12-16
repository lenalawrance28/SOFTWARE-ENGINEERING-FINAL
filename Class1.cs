using System;
using MySqlConnector;
using System.Windows.Forms;


namespace Software_Engineering1
{
    internal class Class1
    {


        public static class BookingHelper // booking helper static class 
        {
            private static string connectionString = "server=localhost;database=theevents;uid=root;pwd=;"; // connection string 

            // used for booking tickets and storing details in database 
            public static void BookTicket( string username ,string eventName, string guestName, string guestEmail, int ticketCount) 
            {
                
                using (var conn = new MySqlConnection(connectionString)) 
                {
                    try
                    {
                        conn.Open(); // my sql database connection open 

                        string insertBookingQuery = @"
                    INSERT INTO user_bookings (username, event_name, booking_time, ticket_count)
                    VALUES (@username, @eventName, @bookingTime, @ticketCount)"; // sql query used to insert booking details into MySql Database

                        using (var cmd = new MySqlCommand(insertBookingQuery, conn))
                        {
                            // parameterized querys to prevent SQL injections 
                            cmd.Parameters.AddWithValue("@username", username); 
                            cmd.Parameters.AddWithValue("@eventName", eventName);
                            cmd.Parameters.AddWithValue("@bookingTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ticketCount", ticketCount);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Successfully booked {ticketCount} ticket(s) for {eventName}!"); // message pop up for sucessfull booking 
                    }
                    catch (Exception ex) // error handeling 
                    {
                        MessageBox.Show("Error booking ticket: " + ex.Message);
                    }
                }
            }

   

            public static EventDetails RetrieveEvent(string eventName)
            {
                using (var conn = new MySqlConnection(connectionString)) //sql connection 
                {
                    try
                    {
                        conn.Open(); // open connection
                        //sql query to retrieve event details using event name 
                        string retrieveeventQuery = @"
                SELECT event_id, event_name, event_description, event_date, event_time
                FROM events1
                WHERE event_name = @eventName";

                        using (var cmd = new MySqlCommand(retrieveeventQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@eventName", eventName); // add parameters 

                            using (var reader = cmd.ExecuteReader()) //execute query and process results 
                            {
                                if (reader.Read()) //check if event is available 
                                {
                                    return new EventDetails
                                    {
                                        EventId = reader.GetInt32("event_id"), //get event id
                                        EventName = reader.GetString("event_name"), // get event name 
                                        EventDescription = reader.GetString("event_description"), // get event description 
                                        EventDate = reader.GetDateTime("event_date"), // get event date
                                        EventTime = reader.GetTimeSpan("event_time")// get event time 
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception ex) //error handling 
                    {
                        MessageBox.Show("Error retrieving event: " + ex.Message); // error message if failure 
                    }
                }

                return null; // Return null if  event is not found or if  an error occurs
            }

        }

    }




}

