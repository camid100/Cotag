using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLLocations
    {
        public IQueryable<DataLayer.Views.LocationsView> GetLocations()
        {
            return new DLLocations().GetLocations();
        }

        public IQueryable<DataLayer.Views.LocationsView> GetLogicallyDeletedLocations()
        {
            return new DLLocations().GetLogicallyDeletedLocations();
        }

        public tb_Location GetLocationNo(int no)
        {
            return new DLLocations().GetLocationNo(no);
        }

        public tb_Location GetLocationDesc(string desc)
        {
            return new DLLocations().GetLocationDesc(desc);
        }

        public IQueryable<DataLayer.Views.LocationsView> GetLocationBySiteID(int no)
        {

            return new DLLocations().GetLocationBySiteID(no);

        }
        private bool CheckLocation(int no)
        {
            tb_Location TempLocation = GetLocationNo(no);
            if (TempLocation == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckLocation(string desc)
        {
            tb_Location TempLocation = GetLocationDesc(desc);
            if (TempLocation == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateLocation(tb_Location newLocation)
        {
            try
            {
                bool LocationNotExists = CheckLocation(newLocation.ID);

                if (LocationNotExists && CheckLocation(newLocation.Description))
                {
                    new DLLocations().CreateLocations(newLocation);
                    return Util.OperationStatus.successful;
                }
                else
                {
                    return Util.OperationStatus.Exists;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus UpdateLocation(tb_Location Location)
        {
            try
            {
                bool ETNotExists = CheckLocation(Location.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLLocations().UpdateLocation(Location);
                    return Util.OperationStatus.successful;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }
    }
}

