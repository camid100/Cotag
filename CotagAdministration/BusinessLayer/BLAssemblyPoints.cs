using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLAssemblyPoints
    {
        public IQueryable<DataLayer.Views.AssemblyPointView> GetAssemblyPoints()
        {
            return new DLAssemblyPoints().GetAssemblyPoints();
        }

        public IQueryable<DataLayer.Views.AssemblyPointView> GetLogicallyDeletedAssemblyPoints()
        {
            return new DLAssemblyPoints().GetLogicallyDeletedAssemblyPoints();
        }

        public tb_AssemblyPoint GetAssemblyPointNo(int no)
        {
            return new DLAssemblyPoints().GetAssemblyPointNo(no);
        }

        public tb_AssemblyPoint GetAssemblyPointDesc(string desc)
        {
            return new DLAssemblyPoints().GetAssemblyPointDesc(desc);
        }


        private bool CheckAssemblyPoint(int no)
        {
            tb_AssemblyPoint TempAssemblyPoint = GetAssemblyPointNo(no);
            if (TempAssemblyPoint == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckAssemblyPoint(string desc)
        {
            tb_AssemblyPoint TempAssemblyPoint = GetAssemblyPointDesc(desc);
            if (TempAssemblyPoint == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Util.OperationStatus CreateAssemblyPoint(tb_AssemblyPoint newAssemblyPoint)
        {
            try
            {
                bool AssemblyPointNotExists = CheckAssemblyPoint(newAssemblyPoint.ID);

                if (AssemblyPointNotExists && CheckAssemblyPoint(newAssemblyPoint.Description))
                {
                    new DLAssemblyPoints().CreateAssemblyPoints(newAssemblyPoint);
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

        public Util.OperationStatus UpdateAssemblyPoint(tb_AssemblyPoint AssemblyPoint)
        {
            try
            {
                bool ETNotExists = CheckAssemblyPoint(AssemblyPoint.ID);

                if (ETNotExists)
                {
                    return Util.OperationStatus.Unsuccessful;
                }
                else
                {
                    new DLAssemblyPoints().UpdateAssemblyPoint(AssemblyPoint);
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
