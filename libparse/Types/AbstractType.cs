using libparse;
using System;
using System.Collections.Generic;
using System.Text;

namespace libxcm.Types
{
    public abstract class AbstractType : AbstractDataClass, IType
    {
        public abstract object InternalData { get; }

        public override IEnumerator<KeyValuePair<string, IDataClass>> GetEnumerator()
        {
            yield return new KeyValuePair<string, IDataClass>(Name, this);
        }
    }
}
