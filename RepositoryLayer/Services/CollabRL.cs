using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        private readonly IMongoCollection<CollabModel> Collab;

        private readonly IConfiguration configuration;

        public CollabRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);

            Collab = database.GetCollection<CollabModel>("Collab");
        }

        public CollabModel AddCollab(CollabModel addcollab, string userid)
        {
            try
            {
                var ifExists = this.Collab.Find(x => x.CollabID == addcollab.CollabID && x.UserID == userid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.Collab.InsertOne(addcollab);
                    return addcollab;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteCollab(string id)
        {
            try
            {
                var ifExists = this.Collab.FindOneAndDelete(x => x.CollabID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<CollabModel> GetAllCollab()
        {
            return Collab.Find(FilterDefinition<CollabModel>.Empty).ToList();
        }
    }
}
