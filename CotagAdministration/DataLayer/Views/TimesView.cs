using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class TimesView
    {
        public int ID { get; set; }
        public string Description{ get; set; }

        public bool IsActive { get; set; }
        public bool IsLogicallyDeleted { get; set; }

        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }

        public string Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }

        public int RecordVersion { get; set; }

        public int startLevel { get; set; }
        public int endLevel { get; set; }
    }
}
