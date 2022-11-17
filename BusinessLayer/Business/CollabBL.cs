using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class CollabBL : ICollabBL
    {

        private readonly ICollabRL collabRL;

        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public CollabModel AddCollab(CollabModel addcollab)
        {
            try
            {
                return collabRL.AddCollab(addcollab);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool DeleteCollab(string id)
        {
            try
            {
                return this.collabRL.DeleteCollab(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CollabModel> GetAllCollab()
        {
            try
            {
                return this.collabRL.GetAllCollab();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

       
    }
}
