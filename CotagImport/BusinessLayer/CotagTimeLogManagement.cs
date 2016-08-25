using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class CotagTimeLogManagement:ConnectionClass
    {
        #region BaseClass
        public CotagTimeLogManagement()
            : base()
        {
        }
        public CotagTimeLogManagement(string str)
            : base(str)
        {
        }
        #endregion
        #region Insert

        public void insertCotagTimeLog(long cotag, int location,string status,DateTime date,DateTime time,string createdBy)
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
                SqlCommand cmdInsert = MyConnection.CreateCommand();
                cmdInsert.CommandText = "usp_InsertCotagTimeLog";
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.Parameters.AddWithValue("@cotag", cotag);
                cmdInsert.Parameters.AddWithValue("@location", location);
                cmdInsert.Parameters.AddWithValue("@status", status.Substring(0,status.IndexOf(' ')));
                cmdInsert.Parameters.AddWithValue("@date", date);
                cmdInsert.Parameters.AddWithValue("@time", time);
                cmdInsert.Parameters.AddWithValue("@createdBy", createdBy);
                cmdInsert.ExecuteNonQuery();

                if (connectionOpenedHere == true)
                    MyConnection.Close();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        #endregion
        #region Select

        public bool checkTimeLog(DateTime logDate,DateTime logTime,long cardNo,string location)
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
                cmdSelect.CommandText = "usp_checkLogTime";
                cmdSelect.CommandType = CommandType.StoredProcedure;
                if (MyTransaction != null)
                    cmdSelect.Transaction = MyTransaction;
                cmdSelect.Parameters.AddWithValue("@logDate", logDate);
                cmdSelect.Parameters.AddWithValue("@logTime", logTime);
                cmdSelect.Parameters.AddWithValue("@cardNo", cardNo);
                cmdSelect.Parameters.AddWithValue("@location", location);


                int i = Convert.ToInt16(cmdSelect.ExecuteScalar());

                if (connectionOpenedHere == true)
                    MyConnection.Close();


                if (i == 0) return false; else return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}
