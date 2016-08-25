using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLContactsGeneration : ConnectionClass
    {
        public IQueryable<Views.ContactsGenerationView> GetContactsGeneration()
        {
            var list = from ent in Entity.tb_contacts_generation_log
                       select new Views.ContactsGenerationView
                       {
                           log_pk = ent.log_pk,
                           processed_date = ent.processed_date,
                           request_date = ent.request_date,
                           status = ent.status,
                           status_detail = ent.status_detail
                       };


            return list.AsQueryable();
        }

        public void CreateContactsGeneration(tb_contacts_generation_log s)
        {
            this.Entity.AddTotb_contacts_generation_log(s);
            this.Entity.SaveChanges();
        }

        public int CheckPendingTask()
        {
            var list = from ent in Entity.tb_contacts_generation_log
                       where ent.status == "P"
                       select new Views.ContactsGenerationView
                       {
                           log_pk = ent.log_pk,
                           processed_date = ent.processed_date,
                           request_date = ent.processed_date,
                           status = ent.status,
                           status_detail = ent.status_detail
                       };


            return list.AsQueryable().Count();
        }
    }
}
