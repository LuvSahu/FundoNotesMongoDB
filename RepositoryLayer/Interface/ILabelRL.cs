using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelModel AddLabel(LabelModel addlabel);

        public IEnumerable<LabelModel> GetAllLabel();

        public LabelModel UpdateLabel(LabelModel editlabel, string id);

        public bool DeleteLabel(string id);


    }
}
