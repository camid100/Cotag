using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.EntityClient;
namespace DataLayer
{
    public class ConnectionClass
    {
        public db_mita_cotagEntities Entity { get; set; }
        public ConnectionClass()
        {
            Entity = new db_mita_cotagEntities();
        }

        public SqlConnection getConnection()
        {
            string strConn = ConfigurationManager.ConnectionStrings["db_mita_cotagEntities"].ConnectionString;
            EntityConnectionStringBuilder b = new EntityConnectionStringBuilder(strConn);

            return new SqlConnection(b.ProviderConnectionString);
        }

    }

}
