using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLTimes:ConnectionClass
    {
        //Get All
        public IQueryable<Views.TimesView> GetTimes()
        {
            var list = from ent in Entity.tb_Times
                       where ent.IsLogicallyDeleted != true
                       select new Views.TimesView
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
        public IQueryable<Views.TimesView> GetLogicallyDeletedTimes()
        {
            var list = from ent in Entity.tb_Times
                       where ent.IsLogicallyDeleted == true
                       select new Views.TimesView
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
        public tb_Times GetTimesByID(int id)
        {
            return Entity.tb_Times.SingleOrDefault(s => s.ID == id);
        }

        //Create
        public void CreateTime(tb_Times time)
        {
            this.Entity.AddTotb_Times(time);
            this.Entity.SaveChanges();
        }

        //Update
        public void UpdateTime(tb_Times time)
        {
            tb_Times OriginalTime = GetTimesByID(time.ID);
            time.CreatedBy = OriginalTime.CreatedBy;
            time.CreatedOn = OriginalTime.CreatedOn;
            time.RecordVersion = OriginalTime.RecordVersion;
            time.RecordVersion++;
            this.Entity.tb_Times.Attach(OriginalTime);
            this.Entity.tb_Times.ApplyCurrentValues(time);
            this.Entity.SaveChanges();
        }

        //Delete
        public void DeleteTime(int id)
        {
            tb_Times comp = Entity.tb_Times.SingleOrDefault(s => s.ID == id);
            this.Entity.DeleteObject(comp);
            this.Entity.SaveChanges();
        }
    }
}
