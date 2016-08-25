using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class ContactBookView
    {
        public string IDNo { get; set; }
        
        public string AssemblyPoint { get; set; }

        public string telephone { get; set; }
        public string mobile { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Department { get; set; }
        public string Section { get; set; }
        public string Location { get; set; }
        public string Site { get; set; }

        public string Organization { get; set; }
        public string Email { get; set; }
    }
}
