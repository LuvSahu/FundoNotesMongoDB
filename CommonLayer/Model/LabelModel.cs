using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LabelID { get; set; }

        public string LabelName { get; set; }

        [ForeignKey("Notes")]
        public string NotesID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
    }
}
