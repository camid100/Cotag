using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace DataLayer
{
    public class DLDepartments : ConnectionClass
    {
        public IQueryable<Views.DepartmentsView> GetDepartments()
        {
            var list = from ent in Entity.vw_Departments
                       select new Views.DepartmentsView
                       {
                           ID = ent.UniqueGUID,
                           Description = ent.DepartmentMarvalCode
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.DepartmentsView> GetDepartments(Guid id)
        {
            var list = from ent in Entity.vw_Departments
                       where (ent.UniqueGUID.ToString() == id.ToString())
                       //SqlMethods.Like(ent.Surname, "%" + surname + "%") && SqlMethods.Like(ent.Name, "%" + name + "%")
                       select new Views.DepartmentsView
                       {
                           ID = ent.UniqueGUID,
                           Description = ent.DepartmentMarvalCode
                       };


            return list.AsQueryable();
        }

     

    }
}
