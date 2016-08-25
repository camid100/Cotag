using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
namespace BusinessLayer
{
    public class BLCompanies
    {
        public IQueryable<DataLayer.Views.CompaniesView> GetCompanies()
        {
            return new DLCompanies().GetCompanies();
        }

        public IQueryable<DataLayer.Views.CompaniesView> GetLogicallyDeletedCompanies()
        {
            return new DLCompanies().GetLogicallyDeletedSites();
        }

        public tb_Companies GetCompanyNo(int no)
        {
            return new DLCompanies().GetCompanyNo(no);
        }

        public tb_Companies GetCompanyDesc(string desc)
        {
            return new DLCompanies().GetCompanyDesc(desc);
        }


        private bool CheckCompany(int no)
        {
            tb_Companies TempCompany = GetCompanyNo(no);
            if (TempCompany == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCompany(string desc)
        {
            tb_Companies TempCompany = GetCompanyDesc(desc);
            if (TempCompany == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateCompany(tb_Companies newCompany)
        {
            try
            {
                bool CompanyNotExists = CheckCompany(newCompany.ID);

                if (CompanyNotExists && CheckCompany(newCompany.Description))
                {
                    new DLCompanies().CreateSites(newCompany);
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

        public Util.OperationStatus UpdateCompany(tb_Companies Company)
        {
            try
            {
                bool ETNotExists = CheckCompany(Company.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLCompanies().UpdateSite(Company);
                    return Util.OperationStatus.successful;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }
        public Util.OperationStatus DeleteCompany(int Company)
        {
            try
            {
                
                   
                    new DLCompanies().DeleteCompany(Company);
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
