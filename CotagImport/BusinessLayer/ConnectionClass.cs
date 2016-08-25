using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
namespace BusinessLayer
{
    public class ConnectionClass
    {
        static SqlConnection _myConnection;
        public static SqlConnection MyConnection
        {

            get { return _myConnection; }
            set { _myConnection = value; }

        }

        public ConnectionClass()
        {
            string str = @"Data Source=fusd4-lp;Initial Catalog=db_mita_cotag;Integrated Security=True";
            _myConnection = new SqlConnection(str);

        }
        public ConnectionClass(string str)
        {
            _myConnection = new SqlConnection(str);
        }
        static SqlTransaction _myTransaction;
        public static SqlTransaction MyTransaction
        {

            get { return _myTransaction; }

            set { _myTransaction = value; }

        }

    }

}