using libparse;
using System;
using System.Collections.Generic;
using System.Text;

namespace libxcm.Types
{
    public interface IType : IDataClass
    {
        object InternalData { get; }
    }
}
