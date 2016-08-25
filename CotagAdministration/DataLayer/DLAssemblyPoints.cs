using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLAssemblyPoints :ConnectionClass
    {
        public IQueryable<Views.AssemblyPointView> GetAssemblyPoints()
        {
            var list = from ent in Entity.tb_AssemblyPoint
                       where ent.IsLogicallyDeleted != true
                       select new Views.AssemblyPointView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           Site_ID = ent.Site_ID,
                           Site_Name = ent.tb_Site.SiteDescription
                       };


            return list.AsQueryable();
        }
        public tb_AssemblyPoint GetAssemblyPointNo(int no)
        {

            return Entity.tb_AssemblyPoint.SingleOrDefault(s => s.ID == no);

        }

        public tb_AssemblyPoint GetAssemblyPointDesc(string Desc)
        {

            return Entity.tb_AssemblyPoint.SingleOrDefault(s => s.Description == Desc);

        }

        public void CreateAssemblyPoints(tb_AssemblyPoint s)
        {
            this.Entity.AddTotb_AssemblyPoint(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.AssemblyPointView> GetLogicallyDeletedAssemblyPoints()
        {
            var list = from ent in Entity.tb_AssemblyPoint
                       where ent.IsLogicallyDeleted == true
                       select new Views.AssemblyPointView
                       {
                           ID = ent.ID,
                           Description = ent.Description,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           Site_ID = ent.Site_ID,
                           Site_Name = ent.tb_Site.SiteDescription
                       };


            return list.AsQueryable();
        }

        public void UpdateAssemblyPoint(tb_AssemblyPoint AssemblyPoint)
        {
            tb_AssemblyPoint OriginalAssemblyPoint = GetAssemblyPointNo(AssemblyPoint.ID);
            AssemblyPoint.Createdby = OriginalAssemblyPoint.Createdby;
            AssemblyPoint.Createdon = OriginalAssemblyPoint.Createdon;
            AssemblyPoint.RecordVersion = OriginalAssemblyPoint.RecordVersion;
            AssemblyPoint.RecordVersion++;
            this.Entity.tb_AssemblyPoint.Attach(OriginalAssemblyPoint);
            this.Entity.tb_AssemblyPoint.ApplyCurrentValues(AssemblyPoint);
            this.Entity.SaveChanges();
        }
    }
}
