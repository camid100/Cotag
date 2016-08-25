using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using System.Collections;
using System.Linq.Expressions;

namespace DataLayer
{
    public class DLZoneTimes: ConnectionClass
    {
        //Get All
        public IQueryable<Views.ZoneTimesView> GetZoneTimes()
        {
            var list = from ent in Entity.tb_AccessZones
                       where ent.IsLogicallyDeleted != true
                       select new Views.ZoneTimesView
                       {
                           location_id = ent.location_id,
                           time_id = ent.time_id,
                           zone_id = ent.zone_id,
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

        public IQueryable<Views.ZoneTimesDetailsView> GetZoneTimesDetails(ArrayList zones)
        {

            List<string> lists = new List<string>();
            foreach(string s in zones)
            {
                lists.Add(s);
            }
            string[] array = lists.ToArray();
            
            var list = from ent in Entity.tb_AccessZones
                       join zone in Entity.tb_Zones on ent.zone_id equals zone.ID
                       join time in Entity.tb_Times on ent.time_id equals time.ID
                       join location in Entity.tb_Location on ent.location_id equals location.ID
                       join site in Entity.tb_Site on location.Site_ID equals site.SiteNo
                       where ent.IsActive == true && array.Any(name => name.Equals(zone.Description))
                       select new Views.ZoneTimesDetailsView
                       {
                           Location = location.Description,
                           Site = site.SiteDescription,
                           Time = time.Description,
                           Zone = zone.Description,
                           ZoneID = ent.zone_id
                       };

            return list.AsQueryable();
        }


        //Get Deleted
        public IQueryable<Views.ZoneTimesView> GetLogicallyDeletedZoneTimes()
        {
            var list = from ent in Entity.tb_AccessZones
                       where ent.IsLogicallyDeleted == true
                       select new Views.ZoneTimesView
                       {
                           location_id = ent.location_id,
                           time_id = ent.time_id,
                           zone_id = ent.zone_id,
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
        public tb_AccessZones GetZoneTimesByID(int zoneID,int locationID,int timeID)
        {
            return Entity.tb_AccessZones.SingleOrDefault(s => s.zone_id == zoneID && s.location_id == locationID && s.time_id == timeID);
        }
        public tb_AccessZones GetZoneTimesByZoneID(int zoneID)
        {
            return Entity.tb_AccessZones.SingleOrDefault(s => s.zone_id == zoneID);
        }
        //Create
        public void CreateTime(tb_AccessZones zoneTime)
        {
            this.Entity.AddTotb_AccessZones(zoneTime);
            this.Entity.SaveChanges();
        }

        //Update
        public void UpdateZoneTime(tb_AccessZones zoneTime)
        {
            tb_AccessZones OriginalZoneTime = GetZoneTimesByID(zoneTime.zone_id,zoneTime.location_id,zoneTime.time_id);
            zoneTime.CreatedBy = OriginalZoneTime.CreatedBy;
            zoneTime.CreatedOn = OriginalZoneTime.CreatedOn;
            zoneTime.RecordVersion = OriginalZoneTime.RecordVersion;
            zoneTime.RecordVersion++;
            this.Entity.tb_AccessZones.Attach(OriginalZoneTime);
            this.Entity.tb_AccessZones.ApplyCurrentValues(zoneTime);
            this.Entity.SaveChanges();
        }

        ////Delete
        //public void DeleteZoneTime(int id)
        //{
        //    tb_Times comp = Entity.tb_Times.SingleOrDefault(s => s.ID == id);
        //    this.Entity.DeleteObject(comp);
        //    this.Entity.SaveChanges();
        //}
    }
}
