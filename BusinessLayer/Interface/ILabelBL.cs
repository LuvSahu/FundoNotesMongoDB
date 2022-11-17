using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelModel AddLabel(LabelModel addlabel);

        public LabelModel UpdateLabel(LabelModel editlabel, string id);

        public bool DeleteLabel(string id);

        public IEnumerable<LabelModel> GetAllLabel();



    }
}
