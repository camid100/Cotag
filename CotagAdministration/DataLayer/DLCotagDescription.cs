using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLCotagDescription : ConnectionClass
    {
        public IQueryable<Views.CotagDescriptionView> GetCotagDescription()
        {
            var list = from ent in Entity.tb_CotagDescription
                       where ent.IsLogicallyDeleted != true
                       select new Views.CotagDescriptionView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
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
        public tb_CotagDescription GetID(int no)
        {

            return Entity.tb_CotagDescription.SingleOrDefault(s => s.ID == no);

        }

        public tb_CotagDescription GetCotagDescriptionDesc(string Desc)
        {

            return Entity.tb_CotagDescription.SingleOrDefault(s => s.Description == Desc);

        }

        public void CreateCotagDescription(tb_CotagDescription s)
        {
            this.Entity.AddTotb_CotagDescription(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.CotagDescriptionView> GetLogicallyDeletedCotagDescription()
        {
            var list = from ent in Entity.tb_CotagDescription
                       where ent.IsLogicallyDeleted == true
                       select new Views.CotagDescriptionView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
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

        public void UpdateCotagDescription(tb_CotagDescription CotagDescription)
        {
            tb_CotagDescription OriginalCotagDescription = GetID(CotagDescription.ID);
            CotagDescription.Createdby = OriginalCotagDescription.Createdby;
            CotagDescription.Createdon = OriginalCotagDescription.Createdon;
            CotagDescription.RecordVersion = OriginalCotagDescription.RecordVersion;
            CotagDescription.RecordVersion++;
            this.Entity.tb_CotagDescription.Attach(OriginalCotagDescription);
            this.Entity.tb_CotagDescription.ApplyCurrentValues(CotagDescription);
            this.Entity.SaveChanges();
        }


    }
}
