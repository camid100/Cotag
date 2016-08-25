using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class ContactsGenerationView
    {
        public int log_pk { get; set; }
        public DateTime? request_date { get; set; }
        public string status { get; set; }
        public string status_detail { get; set; }
        public DateTime? processed_date { get; set; }
    }
}
