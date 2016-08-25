using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLCompanies : ConnectionClass
    {
        public IQueryable<Views.CompaniesView> GetCompanies()
        {
            var list = from ent in Entity.tb_Companies
                       select new Views.CompaniesView
                       {
                           ID = ent.ID,
                           Description= ent.Description
                       };


            return list.AsQueryable();
        }
        public tb_Companies GetCompanyNo(int no)
        {

            return Entity.tb_Companies.SingleOrDefault(s => s.ID == no);

        }

        public tb_Companies GetCompanyDesc(string Desc)
        {

            return Entity.tb_Companies.SingleOrDefault(s => s.Description== Desc);

        }

        public void CreateSites(tb_Companies s)
        {
            this.Entity.AddTotb_Companies(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.CompaniesView> GetLogicallyDeletedSites()
        {
            var list = from ent in Entity.tb_Companies
                       select new Views.CompaniesView
                       {
                           ID = ent.ID,
                           Description= ent.Description
                       };


            return list.AsQueryable();
        }
        public void DeleteCompany(int no)
        {
            tb_Companies comp = Entity.tb_Companies.SingleOrDefault(s => s.ID == no);
            this.Entity.DeleteObject(comp);
            this.Entity.SaveChanges();
        }
        public void UpdateSite(tb_Companies company)
        {
            tb_Companies OriginalCompany = GetCompanyNo(company.ID);
            this.Entity.tb_Companies.Attach(OriginalCompany);
            this.Entity.tb_Companies.ApplyCurrentValues(company);
            this.Entity.SaveChanges();
        }


    }
}
