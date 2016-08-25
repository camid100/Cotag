using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class CotagDetailView
    {
        public int CotagNo { get; set; }
        public string IDNo{ get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int AssemblyPoint_ID { get; set; }
        public string AssemblyPoint_Name { get; set; }

        public string telephone { get; set; }
        public string mobile { get; set; }

        public int CotagDesc_ID { get; set; }
        public string CotagDesc_Name { get; set; }

        public bool IsActive { get; set; }
        public bool IsLogicallyDeleted { get; set; }

        public string Createdby { get; set; }
        public DateTime Createdon { get; set; }

        public string Updatedby { get; set; }
        public DateTime? Updatedon { get; set; }

        public int RecordVersion { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Type { get; set; }
        public string ImagePath { get; set; }

        public bool isPovider { get; set; }
        public int? service_ID { get; set; }
        public int? company_ID { get; set; }
        public int? projectManager_Cotag { get; set; }
        public Guid? departmentGUID { get; set; }
        public int? site_ID { get; set; }
        public int? site_level { get; set; }
    }
}
