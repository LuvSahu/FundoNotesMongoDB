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
        public LabelModel AddLabel(LabelModel addlabel)
        {
            try
            {
                return labelRL.AddLabel(addlabel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public LabelModel UpdateLabel(LabelModel editlabel, string id)
        {
            try
            {
                return this.labelRL.UpdateLabel(editlabel, id);
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
