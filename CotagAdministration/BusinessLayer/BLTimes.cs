using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLTimes
    {
        public IQueryable<DataLayer.Views.TimesView> GetTimes()
        {
            return new DLTimes().GetTimes();
        }

        public IQueryable<DataLayer.Views.TimesView> GetLogicallyDeletedTimes()
        {
            return new DLTimes().GetLogicallyDeletedTimes();
        }

        public tb_Times GetTimeByID(int id)
        {
            return new DLTimes().GetTimesByID(id);
        }

        public Util.OperationStatus CreateTime(tb_Times newTime)
        {
            try
            {
                new DLTimes().CreateTime(newTime);
                    return Util.OperationStatus.successful;
               
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }
        
        public Util.OperationStatus UpdateTime(tb_Times time)
        {
            try
            {
                new DLTimes().UpdateTime(time);
                return Util.OperationStatus.successful;
                
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus DeleteTime(int id)
        {
            try
            {
                new DLTimes().DeleteTime(id);
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
