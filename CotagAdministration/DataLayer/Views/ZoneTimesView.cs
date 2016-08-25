using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class ZoneTimesView
    {
        public int zone_id { get; set; }
        public int location_id { get; set; }
        public int time_id { get; set; }
        
        public bool IsActive { get; set; }
        public bool IsLogicallyDeleted { get; set; }

        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }

        public string Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }

        public int RecordVersion { get; set; }
    }
}
