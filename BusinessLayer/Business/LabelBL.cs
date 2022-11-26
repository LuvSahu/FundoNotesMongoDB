using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public LabelModel AddLabel(LabelModel addlabel, string userid)
        {
            try
            {
                return labelRL.AddLabel(addlabel,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public LabelModel UpdateLabel(LabelModel editlabel, string id, string userid)
        {
            try
            {
                return this.labelRL.UpdateLabel(editlabel, id,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteLabel(string id)
        {
            try
            {
                return this.labelRL.DeleteLabel(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<LabelModel> GetAllLabel()
        {
            try
            {
                return this.labelRL.GetAllLabel();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
