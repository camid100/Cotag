using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLContactBook
    {
        public IQueryable<DataLayer.Views.ContactBookView> GetContactBookSearch(string surname, string name)
        {
            return new DLContactBook().GetContactBookSearch(surname, name);
        }

        public IQueryable<DataLayer.Views.ContactBookView> GetContactBookSearchLetter(string surname)
        {
            return new DLContactBook().GetContactBookSearchLetter(surname);
        }

        public IQueryable<DataLayer.Views.ContactBookView> GetContactBook()
        {
            return new DLContactBook().GetContactBook();
        }

        public IQueryable<DataLayer.Views.ContactBookView> GetContactBookSearchName(string name)
        {
            return new DLContactBook().GetContactBookSearchName(name);
        }

        public IQueryable<DataLayer.Views.ContactBookView> GetContactBookSearchSurname(string surname)
        {
            return new DLContactBook().GetContactBookSearchSurname(surname);
        }

    }
}
