using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Collections;

namespace BusinessLayer
{
    public class BLZoneTimes
    {
        public IQueryable<DataLayer.Views.ZoneTimesView> GetZoneTimes()
        {
            return new DLZoneTimes().GetZoneTimes();
        }

        public IQueryable<DataLayer.Views.ZoneTimesDetailsView> GetZoneTimeDetail(ArrayList zones)
        {
            return new DLZoneTimes().GetZoneTimesDetails(zones);
        }


        public IQueryable<DataLayer.Views.ZoneTimesView> GetLogicallyDeletedZoneTimes()
        {
            return new DLZoneTimes().GetLogicallyDeletedZoneTimes();
        }

        public tb_AccessZones GetZoneTimeByID(int zoneID,int locationID,int timeID)
        {
            return new DLZoneTimes().GetZoneTimesByID(zoneID,locationID,timeID);
        }

        public tb_AccessZones GetZoneTimeByZoneID(int zoneID)
        {
            return new DLZoneTimes().GetZoneTimesByZoneID(zoneID);
        }

        public Util.OperationStatus CreateZoneTime(tb_AccessZones newZoneTime)
        {
            try
            {
                new DLZoneTimes().CreateTime(newZoneTime);
                return Util.OperationStatus.successful;

            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus UpdateZoneTime(tb_AccessZones zoneTime)
        {
            try
            {
                new DLZoneTimes().UpdateZoneTime(zoneTime);
                return Util.OperationStatus.successful;

            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        //public Util.OperationStatus DeleteTime(int id)
        //{
        //    try
        //    {
        //        new DLTimes().DeleteTime(id);
        //        return Util.OperationStatus.successful;

        //    }
        //    catch (Exception e)
        //    {
        //        ExceptionHandler.write(e);
        //        return Util.OperationStatus.Unsuccessful;
        //    }
        //}
    }
}
