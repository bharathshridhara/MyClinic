using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyClinic.Models
{
    public class PatientModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        
        public ObservationModel[] Observations { get; set; }
    }
}