using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class SiteManagement:ConnectionClass
    {
        #region BaseClass
        public SiteManagement()
            : base()
        {
        }
        public SiteManagement(string str)
            : base(str)
        {
        }
        #endregion
        #region Select

        public int getSite(string description)
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
                cmdSelect.CommandText = "usp_getSite";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                if (MyTransaction != null)
                    cmdSelect.Transaction = MyTransaction;
                cmdSelect.Parameters.AddWithValue("@description", description);

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

        #endregion
       
    }
}
