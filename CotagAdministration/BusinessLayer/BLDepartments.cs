using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLDepartments
    {
        public IQueryable<DataLayer.Views.DepartmentsView> GetDepartments()
        {
            return new DLDepartments().GetDepartments();
        }

        public IQueryable<DataLayer.Views.DepartmentsView> GetDepartments(Guid id)
        {
            return new DLDepartments().GetDepartments(id);
        }
    }
}
