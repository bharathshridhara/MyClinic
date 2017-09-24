using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MyClinic.Models
{
    public class ObservationModel
    {
        public ObjectId ObservationId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        public string Doctor { get; set; }

        public string[] ObservationItems {get; set;}

        public ObservationModel()
        {
            ObservationId = ObjectId.GenerateNewId();
        }
        
    }
}