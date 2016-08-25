using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLCotagDetails : ConnectionClass
    {
        public IQueryable<Views.CotagDetailView> GetCotagDetail()
        {
            var list = from ent in Entity.tb_CotagDetail
                       where ent.IsLogicallyDeleted != true
                       select new Views.CotagDetailView
                       {
                           IDNo = ent.IDNo,
                           CotagNo = ent.CotagNo,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           AssemblyPoint_ID = ent.tb_AssemblyPoint.ID,
                           AssemblyPoint_Name = ent.tb_AssemblyPoint.Description,
                           CotagDesc_ID = ent.tb_CotagDescription.ID,
                           CotagDesc_Name = ent.tb_CotagDescription.Description,
                           mobile = ent.mobile,
                           telephone = ent.telephone,
                           EndDate = ent.EndDate,
                           StartDate = ent.StartDate,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Type = ent.Type,
                           isPovider = ent.isProvider,
                           service_ID = ent.serviceDesc_ID,
                           company_ID = ent.companyDesc_ID,
                           projectManager_Cotag = ent.projectManager_Cotag,
                           departmentGUID = ent.DepartmentGUID,
                           site_ID = ent.Site_ID,
                           site_level = ent.site_level,
                           ImagePath = ent.image_path

                       };


            return list.AsQueryable();
        }
        public tb_CotagDetail GetID(int no)
        {

            return Entity.tb_CotagDetail.SingleOrDefault(s => s.CotagNo == no);

        }

        public tb_CotagDetail GetCotagDetailIDNo(string id)
        {

            return Entity.tb_CotagDetail.SingleOrDefault(s => s.IDNo == id);

        }

        public void CreateCotagDetail(tb_CotagDetail s)
        {
            this.Entity.AddTotb_CotagDetail(s);
            this.Entity.SaveChanges();
        }

        public IQueryable<Views.CotagDetailView> GetLogicallyDeletedCotagDetail()
        {
            var list = from ent in Entity.tb_CotagDetail
                       where ent.IsLogicallyDeleted == true
                       select new Views.CotagDetailView
                       {
                           IDNo = ent.IDNo,
                           CotagNo = ent.CotagNo,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           AssemblyPoint_ID = ent.tb_AssemblyPoint.ID,
                           AssemblyPoint_Name = ent.tb_AssemblyPoint.Description,
                           CotagDesc_ID = ent.tb_CotagDescription.ID,
                           CotagDesc_Name = ent.tb_CotagDescription.Description,
                           mobile = ent.mobile,
                           telephone = ent.telephone,
                           EndDate = ent.EndDate,
                           StartDate = ent.StartDate,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Type = ent.Type,
                           isPovider = ent.isProvider,
                           service_ID = ent.serviceDesc_ID,
                           company_ID = ent.companyDesc_ID,
                           projectManager_Cotag = ent.projectManager_Cotag,
                           departmentGUID = ent.DepartmentGUID,
                             site_ID = ent.Site_ID,
                           site_level = ent.site_level,
                           ImagePath = ent.image_path
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.CotagZonesView> GetCotagZonesView()
        {
            var list = from ent in Entity.tb_CotagDetail
                       join czone in Entity.tb_CotagZones
                       on ent.CotagNo equals czone.CotagNo
                       join zone in Entity.tb_Zones
                       on czone.ZoneID equals zone.ID
                       where ent.IsLogicallyDeleted != true && czone.IsActive==true && zone.IsActive==true
                       select new Views.CotagZonesView
                       {
                           IDNo = ent.IDNo,
                           CotagNo = ent.CotagNo,
                           CotagDesc_Name = ent.tb_CotagDescription.Description,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Zone = zone.Description,
                           ZoneID = czone.ZoneID
                       };


            return list.AsQueryable();
        }
      

        public void UpdateCotagDetail(tb_CotagDetail CotagDetail)
        {
            tb_CotagDetail OriginalCotagDetail = GetID(CotagDetail.CotagNo);
            if (CotagDetail.image_path == "")
            {
                CotagDetail.image_path = OriginalCotagDetail.image_path;
            }
            CotagDetail.Createdby = OriginalCotagDetail.Createdby;
            CotagDetail.Createdon = OriginalCotagDetail.Createdon;
            CotagDetail.RecordVersion = OriginalCotagDetail.RecordVersion;
            CotagDetail.RecordVersion++;
            this.Entity.tb_CotagDetail.Attach(OriginalCotagDetail);
            this.Entity.tb_CotagDetail.ApplyCurrentValues(CotagDetail);
            this.Entity.SaveChanges();
        }


        public IQueryable<Views.CotagDetailView> GetCotagDetailAdvanced(string id,string cotag,bool isActive,string name,string surname
            ,string type,string start,string end,int assembly,string tel,string mobile,bool isProvider, int services
            ,Guid dep,int loc,int level,int cotagdesc,int company,string projectManager,bool bypassProvider)
        {
            var list = from ent in Entity.tb_CotagDetail
                       where ent.IsActive == isActive
                       select new Views.CotagDetailView
                       {
                           IDNo = ent.IDNo,
                           CotagNo = ent.CotagNo,
                           IsActive = ent.IsActive,
                           IsLogicallyDeleted = ent.IsLogicallyDeleted,
                           Createdby = ent.Createdby,
                           Createdon = ent.Createdon,
                           Updatedby = ent.Updatedby,
                           Updatedon = ent.Updatedon,
                           RecordVersion = ent.RecordVersion,
                           AssemblyPoint_ID = ent.tb_AssemblyPoint.ID,
                           AssemblyPoint_Name = ent.tb_AssemblyPoint.Description,
                           CotagDesc_ID = ent.tb_CotagDescription.ID,
                           CotagDesc_Name = ent.tb_CotagDescription.Description,
                           mobile = ent.mobile,
                           telephone = ent.telephone,
                           EndDate = ent.EndDate,
                           StartDate = ent.StartDate,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Type = ent.Type,
                           isPovider = ent.isProvider,
                           service_ID = ent.serviceDesc_ID,
                           company_ID = ent.companyDesc_ID,
                           projectManager_Cotag = ent.projectManager_Cotag,
                           departmentGUID = ent.DepartmentGUID,
                           site_ID = ent.Site_ID,
                           site_level = ent.site_level,
                           ImagePath = ent.image_path
                       };
            if(id != "")
            {
                list = list.Where(c => c.IDNo == id);
            }
            if (cotag != "")
            {
                int cotagNum = Convert.ToInt32(cotag);
               list = list.Where(c => c.CotagNo == cotagNum);
            }
            if (name != "")
            {
                list = list.Where(c => c.Name == name);
            }
            if (surname != "")
            {
                list = list.Where(c => c.Surname == surname);
            }
            if (type.Contains("Please")== false)
            {
                list = list.Where(c => c.Type == type);
            }
            if (start != "")
            {
                DateTime startDT = Convert.ToDateTime(start);
                list = list.Where(c => c.StartDate == startDT);
            }
            if (end != "")
            {
                DateTime endtDT = Convert.ToDateTime(end);
                list = list.Where(c => c.EndDate == endtDT);
            }
            if (assembly != -1)
            {
                list = list.Where(c => c.AssemblyPoint_ID == assembly);
            }
            if (tel != "")
            {
                list = list.Where(c => c.telephone == tel);
            }
            if (mobile != "")
            {
                list = list.Where(c => c.mobile == mobile);
            }

            if (bypassProvider == false)
            {
                list = list.Where(c => c.isPovider == isProvider);
            }

            if (services != -1)
            {
                list = list.Where(c => c.service_ID == services);
            }
            if (dep != new Guid())
            {
                list = list.Where(c => c.departmentGUID == dep);
            }
            if (loc != -1)
            {
                list = list.Where(c => c.site_ID == loc);
            }
            if (level != -1)
            {
                list = list.Where(c => c.site_level == level);
            }
            if (cotagdesc != -1)
            {
                list = list.Where(c => c.CotagDesc_ID == cotagdesc);
            }
            if (assembly != -1)
            {
                list = list.Where(c => c.AssemblyPoint_ID == assembly);
            }
            if (company != -1)
            {
                list = list.Where(c => c.company_ID == company);
            }
            if (projectManager != "")
            {
                int prManager = Convert.ToInt32(projectManager);
                list = list.Where(c => c.projectManager_Cotag == prManager);
            }
            return list.AsQueryable();
        }
    }
}
