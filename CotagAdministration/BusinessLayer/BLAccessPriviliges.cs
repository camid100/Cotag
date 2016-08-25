using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLAccessPriviliges
    {
        public IQueryable<DataLayer.Views.AccessPriviligesView> GetAssemblyPoints()
        {
            return new DLAccessPriviliges().GetAccessPrivilige();
        }

        public IQueryable<DataLayer.Views.AccessPriviligesView> GetLogicallyDeletedAssemblyPoints()
        {
            return new DLAccessPriviliges().GetLogicallyDeletedAccesPriviliges();
        }

        public tb_AccessPriviliges GetAccessPriviligeNo(int no)
        {
            return new DLAccessPriviliges().GetAccessPriviligeNo(no);
        }
        public tb_AccessPriviliges GetAccessPriviligeNoandAccess(int no,int access)
        {
            return new DLAccessPriviliges().GetAccessPriviligeNoandAccess(no,access);
        }
        public tb_AccessPriviliges GetAccessPriviligeNo(int cotag, int no)
        {
            return new  DLAccessPriviliges().GetAccessPriviligeByAccessandCotag(cotag, no);
        }

        public IQueryable<DataLayer.Views.AccessPriviligeUsersView> GetAccessPriviligesUsers()
        {
            return new DLAccessPriviliges().GetAccessPriviligesUsers();
        }
        public IQueryable<DataLayer.Views.AccessPriviligeUserDetailsView> GetAccessPriviligesUsersDetailed(int username)
        {
            if (username == 0)
            {
                return new DLAccessPriviliges().GetAccessPriviligesUsersDetailed();
            }
            else
            {
                return new DLAccessPriviliges().GetAccessPriviligesUsersDetailed().Where(s=>s.CotagNo==username);
            }            
        }
        public IQueryable<DataLayer.Views.AccessPriviligesView> GetAllAccessPriviligesByUser(int cotag)
        {
            return new DLAccessPriviliges().GetAllAccessPriviligesByUser(cotag);
        }
        public tb_AccessPriviliges GetAccessPriviligeByCotag(int cotag)
        {
            return new DLAccessPriviliges().GetAccessPriviligeByCotagNo(cotag);
        }

        private bool CheckAccessPriviligeByAccessandCotag(int cotag, int no)
        {
            tb_AccessPriviliges TempAssemblyPoint = GetAccessPriviligeNo(cotag, no);
            if (TempAssemblyPoint == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckAccessPrivilige(int no)
        {
            tb_AccessPriviliges TempAssemblyPoint = GetAccessPriviligeNo(no);
            if (TempAssemblyPoint == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckAccessPriviligebyCotag(int desc)
        {
            tb_AccessPriviliges TempAssemblyPoint = GetAccessPriviligeByCotag(desc);
            if (TempAssemblyPoint == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateAccessPrivilige(tb_AccessPriviliges newAccessPrivilige)
        {
            try
            {
                bool AssemblyPointNotExists = CheckAccessPriviligeByAccessandCotag(newAccessPrivilige.CotagNo,newAccessPrivilige.AccessPointID);

                if (AssemblyPointNotExists)
                {
                    new DLAccessPriviliges().CreateAccessPrivilige(newAccessPrivilige);
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

        public Util.OperationStatus UpdateAccessPrivilige(tb_AccessPriviliges accessprivilige)
        {
            try
            {
                bool ETNotExists = CheckAccessPrivilige(accessprivilige.ID);

                if (ETNotExists)
                {
                    new DLAccessPriviliges().CreateAccessPrivilige(accessprivilige);
                    return Util.OperationStatus.successful;
                }
                else
                {
                    new DLAccessPriviliges().UpdateAccessPrivilige(accessprivilige);
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
