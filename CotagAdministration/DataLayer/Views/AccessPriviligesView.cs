using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class AccessPriviligesView
    {
        public int ID { get; set; }
        public int CotagNo { get; set; }

        public int AccesPointsID { get; set; }
        public string Site_Name { get; set; }

        public TimeSpan? accessFrom { get; set; }
        public TimeSpan? accessTo { get; set; }

        public bool IsActive { get; set; }
        public bool IsLogicallyDeleted { get; set; }

        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }

        public string Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }

        public int RecordVersion { get; set; }
    }
}
