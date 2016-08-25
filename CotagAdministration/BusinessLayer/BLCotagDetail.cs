using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLCotagDetail
    {
        public IQueryable<DataLayer.Views.CotagDetailView> GetCotagDetail()
        {
            return new DLCotagDetails().GetCotagDetail();
        }

        public IQueryable<DataLayer.Views.CotagDetailView> GetLogicallyDeletedCotagDetail()
        {
            return new DLCotagDetails().GetLogicallyDeletedCotagDetail();
        }
        public IQueryable<DataLayer.Views.CotagZonesView> GetCotagZones()
        {
            return new DLCotagDetails().GetCotagZonesView();
        }
        public IQueryable<DataLayer.Views.CotagZonesView> SearchCotagZones(int zoneid,int cotagno)
        {
            if ((cotagno != -1) &&(zoneid != -1))
            {
                return new DLCotagDetails().GetCotagZonesView().Where(x => x.CotagNo == cotagno && x.ZoneID == zoneid);
            }
            else if ((zoneid != -1) && (cotagno == -1))
            {
                return new DLCotagDetails().GetCotagZonesView().Where(x => x.ZoneID == zoneid);
            }
            else if ((zoneid == -1) && (cotagno != -1))
            {
                return new DLCotagDetails().GetCotagZonesView().Where(x => x.CotagNo == cotagno);
            }
            else
            {
                return new DLCotagDetails().GetCotagZonesView();
            }
            
        }
        public IQueryable<DataLayer.Views.CotagDetailView> GetCotagDetailAdvanced(string id, string cotag, bool isActive, string name, string surname
            , string type, string start, string end, int assembly, string tel, string mobile, bool isProvider, int services
            , Guid dep, int loc, int level, int cotagdesc, int company, string projectManager,bool bypassProvider)
        {
            return new DLCotagDetails().GetCotagDetailAdvanced(id,cotag,isActive,name,surname, type, start,
                end, assembly, tel, mobile, isProvider, services, dep, loc, level, cotagdesc, company, projectManager, bypassProvider);
        }
        public tb_CotagDetail GetCotagDetailNo(int no)
        {
            return new DLCotagDetails().GetID(no);
        }

        public tb_CotagDetail GetCotagDetailIDNo(string idno)
        {
            return new DLCotagDetails().GetCotagDetailIDNo(idno);
        }

        


        private bool CheckCotagDetail(int no)
        {
            tb_CotagDetail TempCotagDetail = GetCotagDetailNo(no);
            if (TempCotagDetail == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCotagDetail(string idno)
        {
            tb_CotagDetail TempCotagDetail = GetCotagDetailIDNo(idno);
            if (TempCotagDetail == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateCotagDetail(tb_CotagDetail newCotagDetail)
        {
            try
            {

                bool CotagDetailNotExists = CheckCotagDetail(newCotagDetail.CotagNo);

                if (CotagDetailNotExists && CheckCotagDetail(newCotagDetail.IDNo))
                {
                    new DLCotagDetails().CreateCotagDetail(newCotagDetail);
                    return Util.OperationStatus.successful;
                }
                else
                {
                    return Util.OperationStatus.Exists;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }

        public Util.OperationStatus UpdateCotagDetail(tb_CotagDetail cotag)
        {
            try
            {
                bool ETNotExists = CheckCotagDetail(cotag.CotagNo);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLCotagDetails().UpdateCotagDetail(cotag);
                    return Util.OperationStatus.successful;
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.write(e);
                return Util.OperationStatus.Unsuccessful;
            }
        }


    }
}
