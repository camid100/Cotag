using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLCotagDescription
    {
        public IQueryable<DataLayer.Views.CotagDescriptionView> GetCotagDescription()
        {
            return new DLCotagDescription().GetCotagDescription();
        }

        public IQueryable<DataLayer.Views.CotagDescriptionView> GetLogicallyDeletedCotagDescription()
        {
            return new DLCotagDescription().GetLogicallyDeletedCotagDescription();
        }

        public tb_CotagDescription GetCotagDescriptionNo(int no)
        {
            return new DLCotagDescription().GetID(no);
        }

        public tb_CotagDescription GetCotagDescriptionDesc(string desc)
        {
            return new DLCotagDescription().GetCotagDescriptionDesc(desc);
        }


        private bool CheckCotagDescription(int no)
        {
            tb_CotagDescription TempCotagDescription = GetCotagDescriptionNo(no);
            if (TempCotagDescription == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCotagDescription(string desc)
        {
            tb_CotagDescription TempCotagDescription = GetCotagDescriptionDesc(desc);
            if (TempCotagDescription == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateCotagDescription(tb_CotagDescription newCotagDescription)
        {
            try
            {
                bool CotagDescriptionNotExists = CheckCotagDescription(newCotagDescription.ID);

                if (CotagDescriptionNotExists && CheckCotagDescription(newCotagDescription.Description))
                {
                    new DLCotagDescription().CreateCotagDescription(newCotagDescription);
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

        public Util.OperationStatus UpdateCotagDescription(tb_CotagDescription site)
        {
            try
            {
                bool ETNotExists = CheckCotagDescription(site.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLCotagDescription().UpdateCotagDescription(site);
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
