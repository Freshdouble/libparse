using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace libxcm.Types
{
    public enum FloatingPointType
    {
        @float,
        @double
    }
    public class FloatingPointNumber : AbstractNumericalType
    {
        public double value = 0;
        private readonly int byteLength;
        public FloatingPointNumber(FloatingPointType type)
        {
            FloatingPointType = type;
            switch (type)
            {
                case FloatingPointType.@float:
                    byteLength = sizeof(float);
                    break;
                case FloatingPointType.@double:
                    byteLength = sizeof(double);
                    break;
                default:
                    throw new ArgumentException("Unkown floatingpoint type: " + type.ToString());
            }
            value = 0;
        }

        public FloatingPointType FloatingPointType { get; private set; }

        public override object InternalData => value;

        public override int ByteLength => byteLength;

        public override string ToString() => value.ToString(CultureInfo.InvariantCulture);

        public override int GetBytes(Span<byte> data)
        {
            switch (FloatingPointType)
            {
                case FloatingPointType.@float:
                    if(BitConverter.TryWriteBytes(data, (float)value))
                    {
                        return sizeof(float);
                    }
                    else
                    {
                        return 0;
                    }
                case FloatingPointType.@double:
                    if (BitConverter.TryWriteBytes(data, (double)value))
                    {
                        return sizeof(double);
                    }
                    else
                    {
                        return 0;
                    }
            }
            throw new ArgumentException("Unkown floatingpoint type");
        }

        public override int ParseBytes(ReadOnlySpan<byte> data)
        {
            int ret = 0;
            try
            {
                switch (FloatingPointType)
                {
                    case FloatingPointType.@float:
                        value = BitConverter.ToSingle(data);
                        ret = sizeof(float);
                        break;
                    case FloatingPointType.@double:
                        value = BitConverter.ToDouble(data);
                        ret = sizeof(double);
                        break;
                    default:
                        throw new ArgumentException("Unkown floatingpoint type");
                }
            }
            catch(ArgumentOutOfRangeException)
            {
            }
            return ret;
        }

        public override double GetTransformed()
        {
            if(HasOutputTransform)
            {
                return OutputTransform(value);
            }
            else
            {
                return value;
            }
        }
    }
}
