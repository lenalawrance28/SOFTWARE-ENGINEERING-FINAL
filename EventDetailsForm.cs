using System;
using System.Drawing;
using System.Windows.Forms;

namespace Software_Engineering1
{
    //SUMMARY 
    // Thisfrom is mainly used to show details about specific events
    // SUMMARY
    public partial class EventDetailsForm : Form
    {
        // Private fields that store event details 
      
        private string EventName; // Event Name
        private string EventDescription; //Event Description
        private DateTime EventDate; //Event Date 


        public EventDetailsForm(int eventid, string eventname, string eventdescription, DateTime eventdate)
        {
            InitializeComponent();  //initialize form components
            EventName = eventname;  //Assign Event Name 
            EventDescription = eventdescription; //Assign event description 
            EventDate = eventdate; // assign event date 
        }

        
        
        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            
            label1.Text = $"{EventName}"; // event name 
            label12.Text = $"Event Date: {EventDate:yyyy-MM-dd}"; // date 
            label4.Text = EventDescription; //description 
            label4.MaximumSize = new Size(300, 0); // 300 pixels wide, unlimited height
        }

    
    }
}
