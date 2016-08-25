using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
namespace BusinessLayer
{
    public class BLServices
    {
        public IQueryable<DataLayer.Views.ServicesView> GetServices()
        {
            return new DLServices().GetServices();
        }

        public IQueryable<DataLayer.Views.ServicesView> GetLogicallyDeletedServices()
        {
            return new DLServices().GetLogicallyDeletedSites();
        }

        public tb_Services GetServiceNo(int no)
        {
            return new DLServices().GetServiceNo(no);
        }

        public tb_Services GetServiceDesc(string desc)
        {
            return new DLServices().GetServiceDesc(desc);
        }


        private bool CheckService(int no)
        {
            tb_Services TempService = GetServiceNo(no);
            if (TempService == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckService(string desc)
        {
            tb_Services TempService = GetServiceDesc(desc);
            if (TempService == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateService(tb_Services newService)
        {
            try
            {
                bool ServiceNotExists = CheckService(newService.ID);

                if (ServiceNotExists && CheckService(newService.Description))
                {
                    new DLServices().CreateSites(newService);
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

        public Util.OperationStatus UpdateService(tb_Services Service)
        {
            try
            {
                bool ETNotExists = CheckService(Service.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLServices().UpdateSite(Service);
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
