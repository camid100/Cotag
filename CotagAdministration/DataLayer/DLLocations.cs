using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLLocations : ConnectionClass
    {
        public IQueryable<Views.LocationsView> GetLocations()
        {
            var list = from ent in Entity.tb_Location
                       where ent.IsLogicallyDeleted != true
                       select new Views.LocationsView
                       {
                           ID = ent.ID,
                           SysAddress = ent.SysAddress,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           IsEntry = ent.IsEntry,
                           IsExit = ent.IsExit,
                           Site_ID = ent.Site_ID,
                           Site_Name = ent.tb_Site.SiteDescription
                       };


            return list.AsQueryable();
        }

        public tb_Location GetLocationNo(int no)
        {

            return Entity.tb_Location.SingleOrDefault(s => s.ID == no);

        }

        public tb_Location GetLocationDesc(string Desc)
        {

            return Entity.tb_Location.SingleOrDefault(s => s.Description == Desc);

        }
      

        public IQueryable<Views.LocationsView> GetLocationBySiteID(int no)
        {
            var list = from ent in Entity.tb_Location
                       where ent.Site_ID == no
                       select new Views.LocationsView
                       {
                           ID = ent.ID,
                           SysAddress = ent.SysAddress,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           IsEntry = ent.IsEntry,
                           IsExit = ent.IsExit,
                           Site_ID = ent.Site_ID,
                           Site_Name = ent.tb_Site.SiteDescription
                       };


            return list.AsQueryable().Where(x => x.IsActive==true);
        }
        public void CreateLocations(tb_Location s)
        {
            this.Entity.AddTotb_Location(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.LocationsView> GetLogicallyDeletedLocations()
        {
            var list = from ent in Entity.tb_Location
                       where ent.IsLogicallyDeleted == true
                       select new Views.LocationsView
                       {
                           ID = ent.ID,
                           SysAddress = ent.SysAddress,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           IsEntry = ent.IsEntry,
                           IsExit = ent.IsExit,
                           Site_ID = ent.Site_ID,
                           Site_Name = ent.tb_Site.SiteDescription
                       };


            return list.AsQueryable();
        }

        public void UpdateLocation(tb_Location Location)
        {
            tb_Location OriginalLocation = GetLocationNo(Location.ID);
            Location.Createdby = OriginalLocation.Createdby;
            Location.Createdon = OriginalLocation.Createdon;
            Location.RecordVersion = OriginalLocation.RecordVersion;
            Location.RecordVersion++;
            this.Entity.tb_Location.Attach(OriginalLocation);
            this.Entity.tb_Location.ApplyCurrentValues(Location);
            this.Entity.SaveChanges();
        }

    }
}
