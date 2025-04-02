using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace libxcm.Types
{
    public class Bool : AbstractType, IEquatable<Bool>, IEquatable<bool>
    {
        public bool value;
        public int Bitlength { get; set; } = 1;

        public bool Signed => false;

        public override object InternalData => value;

        public override int ByteLength => 1;

        public bool Equals(Bool other) => other.value == value;

        public static bool operator ==(Bool lhs, Bool rhs) => lhs != null && lhs.Equals(rhs);
        public static bool operator !=(Bool lhs, Bool rhs) => !(lhs == rhs);

        public static implicit operator bool(Bool b) => b.value;

        public override bool Equals(object obj) => Equals(obj as Bool);

        public override int GetHashCode() => value.GetHashCode();

        public bool Equals(bool other) => value == other;

        public override int GetBytes(Span<byte> data)
        {
            if(data.Length > 0)
            {
                data[0] = (byte)(value ? 1 : 0);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int ParseBytes(ReadOnlySpan<byte> data)
        {
            if(data.Length >= 1)
            {
                value = data[0] != 0;
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
