using libparse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace libxcm.Types
{
    public abstract class AbstractNumericalType : AbstractType
    {
        public Func<double, double> OutputTransform {get;set;} = null;
        public bool HasOutputTransform => OutputTransform != null;
        public abstract double GetTransformed();

        public double TransformedData => GetTransformed();
    }
}
