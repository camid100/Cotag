using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;

namespace DataLayer
{
    public class DLContactBook : ConnectionClass
    {
        public IQueryable<Views.ContactBookView> GetContactBook()
        {
            var list = from ent in Entity.vw_CotagDetail
                       select new Views.ContactBookView
                       {
                           IDNo = ent.IDNo,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Department = ent.DepartmentFullName,
                           Section = ent.SectionFullName,
                           telephone = ent.telephone,
                           mobile = ent.mobile,
                           Location = ent.LocationCode,
                           Site = ent.SiteName,
                           AssemblyPoint = ent.AssemblyPoint,
                           Organization = ent.OrganisationName,
                           Email = ent.ContactEmail
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.ContactBookView> GetContactBookSearch(string surname, string name)
        {
            var list = from ent in Entity.vw_CotagDetail
                       where (ent.Surname.Contains(surname) && ent.Name.Contains(name))
                           //SqlMethods.Like(ent.Surname, "%" + surname + "%") && SqlMethods.Like(ent.Name, "%" + name + "%")
                       select new Views.ContactBookView
                       {
                           IDNo = ent.IDNo,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Department = ent.DepartmentFullName,
                           Section = ent.SectionFullName,
                           telephone = ent.telephone,
                           mobile = ent.mobile,                           
                           Location = ent.LocationCode,                           
                           Site = ent.SiteName,
                           AssemblyPoint = ent.AssemblyPoint,
                           Organization = ent.OrganisationName,
                           Email = ent.ContactEmail
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.ContactBookView> GetContactBookSearchLetter(string surname)
        {
            var list = from ent in Entity.vw_CotagDetail
                       where ent.Surname.StartsWith(surname)
                       select new Views.ContactBookView
                       {
                           IDNo = ent.IDNo,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Department = ent.DepartmentFullName,
                           Section = ent.SectionFullName,
                           telephone = ent.telephone,
                           mobile = ent.mobile,
                           Location = ent.LocationCode,
                           Site = ent.SiteName,
                           AssemblyPoint = ent.AssemblyPoint,
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.ContactBookView> GetContactBookSearchSurname(string surname)
        {
            var list = from ent in Entity.vw_CotagDetail
                       where ent.Surname.Contains(surname)
                       select new Views.ContactBookView
                       {
                           IDNo = ent.IDNo,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Department = ent.DepartmentFullName,
                           Section = ent.SectionFullName,
                           telephone = ent.telephone,
                           mobile = ent.mobile,
                           Location = ent.LocationCode,
                           Site = ent.SiteName,
                           AssemblyPoint = ent.AssemblyPoint,
                           Organization = ent.OrganisationName,
                           Email = ent.ContactEmail
                       };


            return list.AsQueryable();
        }

        public IQueryable<Views.ContactBookView> GetContactBookSearchName(string name)
        {
            var list = from ent in Entity.vw_CotagDetail
                       where ent.Name.Contains(name)
                       select new Views.ContactBookView
                       {
                           IDNo = ent.IDNo,
                           Name = ent.Name,
                           Surname = ent.Surname,
                           Department = ent.DepartmentFullName,
                           Section = ent.SectionFullName,
                           telephone = ent.telephone,
                           mobile = ent.mobile,
                           Location = ent.LocationCode,
                           Site = ent.SiteName,
                           AssemblyPoint = ent.AssemblyPoint,
                           Organization = ent.OrganisationName,
                           Email = ent.ContactEmail
                       };


            return list.AsQueryable();
        }

    }
}
