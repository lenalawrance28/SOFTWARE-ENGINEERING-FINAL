using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static Software_Engineering1.Class1;
using static Software_Engineering1.DashboardForm;


namespace Software_Engineering1
{

    // SUMMARY
    // THis form is reponsible for handeling ticket booking 
    // for different events
    //SUMMARY
    public partial class BookingForm : Form
    {

        // private fields to store event details 
        private string EventName;
        private string EventDescription;
        private DateTime EventDate;
        public BookingForm() //initializes the BookingForm
        {
            InitializeComponent();
        }

        //constructor to initialize BookingForm with event details 
        public BookingForm(int eventid, string eventname, string eventdescription, DateTime eventdate)
        {


            EventName = eventname;                  // provides event name 
            EventDescription = eventdescription;    // provides description
            EventDate = eventdate;                  // provides event date 
        }

     

        public static class SharedData //  Static class used to store shared data 
        {
            public static string LoggedInUser { get; set; } // stores username of logged in user 
        }
    
        
        private void button7_Click(object sender, EventArgs e)
        {
            //retrieves details required for booking 
            string username = LoggedInUser.Username;   //username of logged in user 
            string eventName = label1.Text;            // Event name displayed
            string guestName = textBox1.Text;          // Guest name from textBox1
            string guestEmail = textBox2.Text;         // Guest email from textBox2
            int ticketCount = (int)numericUpDown1.Value; //Numerical value changer
            // calls helper function to book tickets 
            Class1.BookingHelper.BookTicket( username, eventName, guestName, guestEmail, ticketCount);
        }


        internal void LoadEventDetail(String eventname)
        {
            //Retreive event details using Booking helper 

            var eventDetails = BookingHelper.RetrieveEvent(eventname);
            if (eventDetails != null)
            {

                // displays label forms with event details 
                label1.Text = $"{eventDetails.EventName}";
                label14.Text = $"Date: {eventDetails.EventDate.ToShortDateString()}";
                label4.Text = $"Description: {eventDetails.EventDescription}";
          
            }
            else
            {
                MessageBox.Show("Event not found."); //if event details are not found displays message box
            }

        }




    }
}
