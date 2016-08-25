using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace BusinessLayer
{
    public class BLContactsGeneration
    {
        public int checkPendingTasks()
        {
            return new DLContactsGeneration().CheckPendingTask();
        }

        public IQueryable<DataLayer.Views.ContactsGenerationView> GetAssemblyPoints()
        {
            return new DLContactsGeneration().GetContactsGeneration();
        }

        public Util.OperationStatus CreateContactsGeneration(tb_contacts_generation_log newContact)
        {
            try
            {
                int pendingTasks = checkPendingTasks();

                if (pendingTasks == 0)
                {
                    new DLContactsGeneration().CreateContactsGeneration(newContact);
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
    }
}
