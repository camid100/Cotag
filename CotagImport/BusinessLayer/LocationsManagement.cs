using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class LocationsManagement:ConnectionClass
    {
        #region BaseClass
        public LocationsManagement()
            : base()
        {
        }
        public LocationsManagement(string str)
            : base(str)
        {
        }

        #endregion

        public int getLocation(string sysAddress,int siteID)
        {
            try
            {
                bool connectionOpenedHere = false;

                if (MyConnection.State == ConnectionState.Open)
                    connectionOpenedHere = false;
                else
                {
                    connectionOpenedHere = true;
                    MyConnection.Open();
                }

                SqlCommand cmdSelect = MyConnection.CreateCommand();
                cmdSelect.CommandText = "usp_getLocation";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                if (MyTransaction != null)
                    cmdSelect.Transaction = MyTransaction;
                cmdSelect.Parameters.AddWithValue("@sysAddress", sysAddress);
                cmdSelect.Parameters.AddWithValue("@siteID", siteID);

                int i = Convert.ToInt16(cmdSelect.ExecuteScalar());

                if (connectionOpenedHere == true)
                    MyConnection.Close();


                return i;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
