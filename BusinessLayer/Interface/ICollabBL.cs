using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabModel AddCollab(CollabModel addcollab, string userid);

        public bool DeleteCollab(string id);

        public IEnumerable<CollabModel> GetAllCollab();


    }
}
