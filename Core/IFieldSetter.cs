using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbwpf.Core
{
    public interface IFieldSetter
    {
        public bool SetField<TField>(ref TField field, TField value, string propertyName);
    }
}
