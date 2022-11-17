using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NotesID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

       public string Color { get; set; }

        public string Image { get; set; }

        public bool Archive { get; set; }

        public bool Pin { get; set; }

        public bool Trash { get; set; }

        public DateTime Reminder { get; set; }

        public DateTime CreateTable { get; set; }

        public DateTime EditedTime { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

    }
}
