using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyClinic.Models
{
    public class DataContext
    {
        private IMongoDatabase _db;

        public IMongoCollection<PatientModel> Patients => _db.GetCollection<PatientModel>("patients");

        public DataContext()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            _db = client.GetDatabase(ConfigurationManager.AppSettings.GetValues("DatabaseName").FirstOrDefault());
        }


    }
}