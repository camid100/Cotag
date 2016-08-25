using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLZones : ConnectionClass
    {
        //Get All
        public IQueryable<Views.ZonesView> GetZones()
        {
            var list = from ent in Entity.tb_Zones
                       where ent.IsLogicallyDeleted != true
                       select new Views.ZonesView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.CreatedBy,
                           Createdon = ent.CreatedOn,
                           Updatedby = ent.UpdatedBy,
                           Updatedon = ent.UpdatedOn,
                           RecordVersion = ent.RecordVersion
                       };
            return list.AsQueryable();
        }

        //Get Deleted
        public IQueryable<Views.ZonesView> GetLogicallyDeletedZones()
        {
            var list = from ent in Entity.tb_Zones
                       where ent.IsLogicallyDeleted == true
                       select new Views.ZonesView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.CreatedBy,
                           Createdon = ent.CreatedOn,
                           Updatedby = ent.UpdatedBy,
                           Updatedon = ent.UpdatedOn,
                           RecordVersion = ent.RecordVersion
                       };
            return list.AsQueryable();
        }

        //By ID - only one
        public tb_Zones GetZonesByID(int id)
        {
            return Entity.tb_Zones.SingleOrDefault(s => s.ID == id);
        }

        //Create
        public void CreateZone(tb_Zones zone)
        {
            this.Entity.AddTotb_Zones(zone);
            this.Entity.SaveChanges();
        }

        //Update
        public void UpdateZone(tb_Zones zone)
        {
            tb_Zones OriginalZone = GetZonesByID(zone.ID);
            zone.CreatedBy = OriginalZone.CreatedBy;
            zone.CreatedOn = OriginalZone.CreatedOn;
            zone.RecordVersion = OriginalZone.RecordVersion;
            zone.RecordVersion++;
            this.Entity.tb_Zones.Attach(OriginalZone);
            this.Entity.tb_Zones.ApplyCurrentValues(zone);
            this.Entity.SaveChanges();
        }

        //Delete
        public void DeleteZone(int id)
        {
            tb_Zones comp = Entity.tb_Zones.SingleOrDefault(s => s.ID == id);
            this.Entity.DeleteObject(comp);
            this.Entity.SaveChanges();
        }
    }
}
