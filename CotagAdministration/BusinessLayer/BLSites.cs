using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
namespace BusinessLayer
{
    public class BLSites
    {
        public IQueryable<DataLayer.Views.SitesView> GetSites()
        {
            return new DLSites().GetSites();
        }

        public IQueryable<DataLayer.Views.SitesView> GetLogicallyDeletedSites()
        {
            return new DLSites().GetLogicallyDeletedSites();
        }

        public tb_Site GetSiteNo(int no)
        {
            return new DLSites().GetSiteNo(no);
        }

        public tb_Site GetSiteDesc(string desc)
        {
            return new DLSites().GetSiteDesc(desc);
        }


        private bool CheckSite(int no)
        {
            tb_Site TempSite = GetSiteNo(no);
            if (TempSite == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckSite(string desc)
        {
            tb_Site TempSite = GetSiteDesc(desc);
            if (TempSite == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateSite(tb_Site newSite)
        {
            try
            {
                bool SiteNotExists = CheckSite(newSite.SiteNo);

                if (SiteNotExists && CheckSite(newSite.SiteDescription))
                {
                    new DLSites().CreateSites(newSite);
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

        public Util.OperationStatus UpdateSite(tb_Site site)
        {
            try
            {
                bool ETNotExists = CheckSite(site.SiteNo);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLSites().UpdateSite(site);
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
