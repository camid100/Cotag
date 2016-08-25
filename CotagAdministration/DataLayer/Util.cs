using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class Util
    {
        public enum OperationStatus
        {
            successful = 1,
            Unsuccessful = 2,
            Exists = 3,
            Denied = 4
        }
    }
}
