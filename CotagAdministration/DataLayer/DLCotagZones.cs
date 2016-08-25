using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class DLCotagZones : ConnectionClass
    {
        public void CreateCotagZone(tb_CotagZones cz)
        {
            this.Entity.AddTotb_CotagZones(cz);
            this.Entity.SaveChanges();
        }

        public tb_CotagZones GetCotagZone(int cotagno,int zoneid)
        {

            return Entity.tb_CotagZones.SingleOrDefault(s => s.CotagNo == cotagno && s.ZoneID == zoneid && s.IsActive == true);

        }

        public void DeleteCotagZone(tb_CotagZones cz)
        {
            tb_CotagZones OriginalCotagZone = GetCotagZone(cz.CotagNo,cz.ZoneID);
            cz.CreatedBy = OriginalCotagZone.CreatedBy;
            cz.CreatedOn = OriginalCotagZone.CreatedOn;
            cz.RecordVersion = OriginalCotagZone.RecordVersion;
            cz.RecordVersion++;
            this.Entity.tb_CotagZones.Attach(OriginalCotagZone);
            this.Entity.tb_CotagZones.ApplyCurrentValues(cz);
            this.Entity.SaveChanges();
        }
    }
}
