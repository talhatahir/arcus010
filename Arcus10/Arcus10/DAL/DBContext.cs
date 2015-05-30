using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arcus10.Models;
using System.Data.Entity;
using System.Configuration;

namespace Arcus10.DAL
{
    public class DBContext
    {

        private string _connection;

        public DBContext()
        {
            _connection = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        }

        public string getConnection(){
            return _connection;
        }




    }
}