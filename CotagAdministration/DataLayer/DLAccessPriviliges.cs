using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLAccessPriviliges:ConnectionClass
    {
        public IQueryable<Views.AccessPriviligesView> GetAccessPrivilige()
        {
            var list = from ent in Entity.tb_AccessPriviliges
                       where ent.IsLogicallyDeleted != true
                       select new Views.AccessPriviligesView
                       {
                           ID = ent.ID,
                           CotagNo = ent.CotagNo,
                           AccesPointsID = ent.AccessPointID,
                           accessFrom = ent.accessFrom,
                           accessTo = ent.accessTo,
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
        public tb_AccessPriviliges GetAccessPriviligeNo(int no)
        {

            return Entity.tb_AccessPriviliges.FirstOrDefault(s => s.ID == no);

        }
        public tb_AccessPriviliges GetAccessPriviligeNoandAccess(int no,int access)
        {
            return Entity.tb_AccessPriviliges.FirstOrDefault(s => s.CotagNo == no && s.AccessPointID==access);
        }
        public tb_AccessPriviliges GetAccessPriviligeByAccessandCotag(int cotag,int accesspoint)
        {

            return Entity.tb_AccessPriviliges.SingleOrDefault(s => s.CotagNo== cotag && s.AccessPointID==accesspoint && s.IsActive== true && s.IsLogicallyDeleted==false);

        }
        public tb_AccessPriviliges GetAccessPriviligeByCotagNo(int cotag)
        {

            return Entity.tb_AccessPriviliges.SingleOrDefault(s => s.CotagNo == cotag);

        }

        public void CreateAccessPrivilige(tb_AccessPriviliges s)
        {
            this.Entity.AddTotb_AccessPriviliges(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.AccessPriviligesView> GetLogicallyDeletedAccesPriviliges()
        {
            var list = from ent in Entity.tb_AccessPriviliges
                       where ent.IsLogicallyDeleted == true
                       select new Views.AccessPriviligesView
                       {
                           ID = ent.ID,
                           CotagNo = ent.CotagNo,
                           AccesPointsID = ent.AccessPointID,
                           accessFrom = ent.accessFrom,
                           accessTo = ent.accessTo,
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
        public IQueryable<Views.AccessPriviligesView> GetAllAccessPriviligesByUser(int cotag)
        {
            var list = from ent in Entity.tb_AccessPriviliges
                       where ent.CotagNo == cotag && ent.IsActive == true && ent.IsLogicallyDeleted == false
                       select new Views.AccessPriviligesView
                       {
                           ID = ent.ID,
                           CotagNo = ent.CotagNo,
                           AccesPointsID = ent.AccessPointID,
                           accessFrom = ent.accessFrom,
                           accessTo = ent.accessTo,
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
        public void UpdateAccessPrivilige(tb_AccessPriviliges AssemblyPoint)
        {
            tb_AccessPriviliges OriginalAccessPrivilige = GetAccessPriviligeNo(AssemblyPoint.ID);
            AssemblyPoint.Createdby = OriginalAccessPrivilige.Createdby;
            AssemblyPoint.Createdon = OriginalAccessPrivilige.Createdon;
            AssemblyPoint.RecordVersion = OriginalAccessPrivilige.RecordVersion;
            AssemblyPoint.RecordVersion++;
            this.Entity.tb_AccessPriviliges.Attach(OriginalAccessPrivilige);
            this.Entity.tb_AccessPriviliges.ApplyCurrentValues(AssemblyPoint);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.AccessPriviligeUsersView> GetAccessPriviligesUsers()
        {
            var list = from ent in Entity.vw_getAccessPriviligeUsers
                       select new Views.AccessPriviligeUsersView
                       {
                           accessFrom = ent.accessFrom,
                           accessTo = ent.accessTo,
                           CotagNo = ent.CotagNo,
                           idCard = ent.IDNo,
                           mobile = ent.mobile,
                           telephone = ent.telephone,
                           name = ent.Name,
                           surname = ent.Surname
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.AccessPriviligeUserDetailsView> GetAccessPriviligesUsersDetailed()
        {
            var list = from ent in Entity.vw_getAccessPriviligeUsersDetailed
                       select new Views.AccessPriviligeUserDetailsView
                       {
                           accessFrom = ent.accessFrom,
                           accessTo = ent.accessTo,
                           CotagNo = ent.CotagNo,
                           idCard = ent.IDNo,
                           mobile = ent.mobile,
                           telephone = ent.telephone,
                           name = ent.Name,
                           surname = ent.Surname,
                          siteDescription = ent.SiteDescription,
                          accessDescription = ent.Description
                       };


            return list.AsQueryable();
        }

    }
}
