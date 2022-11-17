using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabModel AddCollab(CollabModel addcollab);

        public bool DeleteCollab(string id);

        public IEnumerable<CollabModel> GetAllCollab();



    }
}
