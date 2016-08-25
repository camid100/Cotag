
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Views
{
    public class AccessPriviligeUserDetailsView
    {
        public int CotagNo { get; set; }

        public TimeSpan? accessFrom { get; set; }
        public TimeSpan? accessTo { get; set; }

        public string idCard { get; set; }
        public string telephone { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mobile { get; set; }
        public string siteDescription { get; set; }
        public string accessDescription { get; set; }
    }
}
