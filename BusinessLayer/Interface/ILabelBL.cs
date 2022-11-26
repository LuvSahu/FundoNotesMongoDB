using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelModel AddLabel(LabelModel addlabel, string userid);

        public LabelModel UpdateLabel(LabelModel editlabel, string id, string userid);

        public bool DeleteLabel(string id);

        public IEnumerable<LabelModel> GetAllLabel();



    }
}
