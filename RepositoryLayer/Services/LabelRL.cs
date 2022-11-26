using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly IMongoCollection<LabelModel> Label;

        private readonly IConfiguration configuration;

        public LabelRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);

            Label = database.GetCollection<LabelModel>("Label");
        }

        public LabelModel AddLabel(LabelModel addlabel, string userid)
        {
            try
            {
                var ifExists = this.Label.Find(x => x.NotesID == addlabel.NotesID && x.UserID == userid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.Label.InsertOne(addlabel);
                    return addlabel;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<LabelModel> GetAllLabel()
        {
            return Label.Find(FilterDefinition<LabelModel>.Empty).ToList();
        }

        public LabelModel UpdateLabel(LabelModel editlabel, string id,string userid)
        {
            try
            {
                var ifExists = this.Label.Find(x => x.LabelID == id && x.UserID == userid).FirstOrDefault();
                if (ifExists != null)
                {

                    this.Label.UpdateOne(x => x.LabelID == id, Builders<LabelModel>.Update.Set(x => x.LabelName, editlabel.LabelName));    
                    return ifExists;
                }
                else
                {
                    this.Label.InsertOne(editlabel);
                    return editlabel;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteLabel(string id)
        {
            try
            {
                var ifExists = this.Label.FindOneAndDelete(x => x.LabelID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
