using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class LocationsView
    {
        public int ID { get; set; }
        public string SysAddress { get; set; }
        public string Description { get; set; }

        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }

        public int Site_ID { get; set; }
        public string Site_Name { get; set; }

      

        public bool IsActive { get; set; }
        public bool IsLogicallyDeleted { get; set; }

        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }

        public string Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }

        public int RecordVersion { get; set; }
    }
}
