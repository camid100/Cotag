using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLCotagZones
    {
        public Util.OperationStatus CreateCotagZone(tb_CotagZones newCotagZone)
        {
            try
            {
                new DLCotagZones().CreateCotagZone(newCotagZone);
                return Util.OperationStatus.successful;                
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus LogicallyDeleteCotagZone(tb_CotagZones newCotagZone)
        {
            try
            {
                new DLCotagZones().DeleteCotagZone(newCotagZone);
                return Util.OperationStatus.successful;
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }
    }
}
