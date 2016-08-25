using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLZones
    {
        public IQueryable<DataLayer.Views.ZonesView> GetZones()
        {
            return new DLZones().GetZones();
        }

        public IQueryable<DataLayer.Views.ZonesView> GetLogicallyDeletedZones()
        {
            return new DLZones().GetLogicallyDeletedZones();
        }

        public tb_Zones GetTimeByID(int id)
        {
            return new DLZones().GetZonesByID(id);
        }

        public Util.OperationStatus CreateZone(tb_Zones newzone)
        {
            try
            {
                new DLZones().CreateZone(newzone);
                return Util.OperationStatus.successful;

            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus UpdateZone(tb_Zones zone)
        {
            try
            {
                new DLZones().UpdateZone(zone);
                return Util.OperationStatus.successful;

            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus DeleteZone(int id)
        {
            try
            {
                new DLZones().DeleteZone(id);
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
