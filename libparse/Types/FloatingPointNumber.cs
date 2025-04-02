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
    public class FloatingPointNumber : AbstractNumericalType, IEquatable<FloatingPointNumber>, IEquatable<float>, IEquatable<double>
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

        public bool Equals(FloatingPointNumber other)
        {
            return value == other.value;
        }

        public static bool operator ==(FloatingPointNumber lhs, FloatingPointNumber rhs) => lhs != null && lhs.Equals(rhs);
        public static bool operator !=(FloatingPointNumber lhs, FloatingPointNumber rhs) => !(lhs == rhs);

        public override bool Equals(object obj) => Equals(obj as FloatingPointNumber);

        public override int GetHashCode() => value.GetHashCode();

        public bool Equals(double other) => value == other;

        public bool Equals(float other) => value == other;

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
            try
            {
                switch (FloatingPointType)
                {
                    case FloatingPointType.@float:
                        value = BitConverter.ToSingle(data);
                        return sizeof(float);
                    case FloatingPointType.@double:
                        value = BitConverter.ToDouble(data);
                        return sizeof(double);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                return 0;
            }
            throw new ArgumentException("Unkown floatingpoint type");
        }

        public static implicit operator float(FloatingPointNumber f) => (float)f.value;
        public static implicit operator double(FloatingPointNumber f) => (double)f.value;
    }
}
