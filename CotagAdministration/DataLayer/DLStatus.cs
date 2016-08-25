using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLStatus : ConnectionClass
    {
        public IQueryable<Views.StatusView> GetStatus()
        {
            var list = from ent in Entity.tb_Status
                       where ent.IsLogicallyDeleted != true
                       select new Views.StatusView
                       {
                           ID = ent.ID,
                           Description = ent.StatusDescription,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion
                       };


            return list.AsQueryable();
        }
        public tb_Status GetID(int no)
        {

            return Entity.tb_Status.SingleOrDefault(s => s.ID == no);

        }

        public tb_Status GetStatusDesc(string Desc)
        {

            return Entity.tb_Status.SingleOrDefault(s => s.StatusDescription == Desc);

        }

        public void CreateStatus(tb_Status s)
        {
            this.Entity.AddTotb_Status(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.StatusView> GetLogicallyDeletedStatus()
        {
            var list = from ent in Entity.tb_Status
                       where ent.IsLogicallyDeleted == true
                       select new Views.StatusView
                       {
                           ID = ent.ID,
                           Description = ent.StatusDescription,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion
                       };


            return list.AsQueryable();
        }

        public void UpdateStatus(tb_Status status)
        {
            tb_Status Originalstatus = GetID(status.ID);
            status.Createdby = Originalstatus.Createdby;
            status.Createdon = Originalstatus.Createdon;
            status.RecordVersion = Originalstatus.RecordVersion;
            status.RecordVersion++;
            this.Entity.tb_Status.Attach(Originalstatus);
            this.Entity.tb_Status.ApplyCurrentValues(status);
            this.Entity.SaveChanges();
        }


    }
}
