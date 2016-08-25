using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLSites : ConnectionClass
    {
        public IQueryable<Views.SitesView> GetSites()
        {
            var list = from ent in Entity.tb_Site
                       where ent.IsLogicallyDeleted != true
                       select new Views.SitesView
                       {
                           SiteNo = ent.SiteNo,
                           SiteDescription = ent.SiteDescription,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                         startLevel = ent.StartLevel,
                         endLevel = ent.EndLevel
                       };


            return list.AsQueryable();
        }
        public tb_Site GetSiteNo(int no)
        {

            return Entity.tb_Site.SingleOrDefault(s => s.SiteNo == no);

        }

        public tb_Site GetSiteDesc(string Desc)
        {

            return Entity.tb_Site.SingleOrDefault(s => s.SiteDescription == Desc);

        }

        public void CreateSites(tb_Site s)
        {
            this.Entity.AddTotb_Site(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.SitesView> GetLogicallyDeletedSites()
        {
            var list = from ent in Entity.tb_Site
                       where ent.IsLogicallyDeleted == true
                       select new Views.SitesView
                       {
                           SiteNo = ent.SiteNo,
                           SiteDescription = ent.SiteDescription,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                               startLevel = ent.StartLevel,
                         endLevel = ent.EndLevel
                       };


            return list.AsQueryable();
        }

        public void UpdateSite(tb_Site site)
        {
            tb_Site OriginalSite = GetSiteNo(site.SiteNo);
            site.Createdby = OriginalSite.Createdby;
            site.Createdon = OriginalSite.Createdon;
            site.RecordVersion = OriginalSite.RecordVersion;
            site.RecordVersion++;
            this.Entity.tb_Site.Attach(OriginalSite);
            this.Entity.tb_Site.ApplyCurrentValues(site);
            this.Entity.SaveChanges();
        }


    }
}
