using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLStatus
    {
        public IQueryable<DataLayer.Views.StatusView> GetStatus()
        {
            return new DLStatus().GetStatus();
        }

        public IQueryable<DataLayer.Views.StatusView> GetLogicallyDeletedStatus()
        {
            return new DLStatus().GetLogicallyDeletedStatus();
        }

        public tb_Status GetStatusNo(int no)
        {
            return new DLStatus().GetID(no);
        }

        public tb_Status GetStatusDesc(string desc)
        {
            return new DLStatus().GetStatusDesc(desc);
        }


        private bool CheckStatus(int no)
        {
            tb_Status TempStatus = GetStatusNo(no);
            if (TempStatus == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckStatus(string desc)
        {
            tb_Status TempStatus = GetStatusDesc(desc);
            if (TempStatus == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateStatus(tb_Status newStatus)
        {
            try
            {
                bool StatusNotExists = CheckStatus(newStatus.ID);

                if (StatusNotExists && CheckStatus(newStatus.StatusDescription))
                {
                    new DLStatus().CreateStatus(newStatus);
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

        public Util.OperationStatus UpdateStatus(tb_Status site)
        {
            try
            {
                bool ETNotExists = CheckStatus(site.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLStatus().UpdateStatus(site);
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
