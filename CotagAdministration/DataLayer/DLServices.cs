using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLServices : ConnectionClass
    {
        public IQueryable<Views.ServicesView> GetServices()
        {
            var list = from ent in Entity.tb_Services
                       select new Views.ServicesView
                       {
                           ID = ent.ID,
                           Description= ent.Description
                       };


            return list.AsQueryable();
        }
        public tb_Services GetServiceNo(int no)
        {

            return Entity.tb_Services.SingleOrDefault(s => s.ID == no);

        }

        public tb_Services GetServiceDesc(string Desc)
        {

            return Entity.tb_Services.SingleOrDefault(s => s.Description == Desc);

        }

        public void CreateSites(tb_Services s)
        {
            this.Entity.AddTotb_Services(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.ServicesView> GetLogicallyDeletedSites()
        {
            var list = from ent in Entity.tb_Services
                       select new Views.ServicesView
                       {
                           ID = ent.ID,
                           Description= ent.Description
                       };


            return list.AsQueryable();
        }

        public void UpdateSite(tb_Services Service)
        {
            tb_Services OriginalService = GetServiceNo(Service.ID);
            Service.Description = OriginalService.Description;
            this.Entity.tb_Services.Attach(OriginalService);
            this.Entity.tb_Services.ApplyCurrentValues(Service);
            this.Entity.SaveChanges();
        }


    }
}
