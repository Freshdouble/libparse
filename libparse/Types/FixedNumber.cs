using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace libxcm.Types
{
    public class FixedNumber : AbstractNumericalType
    {
        private readonly int bitlength;
        private BigInteger data;

        public FixedNumber(int bitlength, bool signed) : this(bitlength, signed, 0)
        {

        }
        public FixedNumber(int bitlength, bool signed, BigInteger value)
        {
            if(bitlength % 8 != 0)
            {
                throw new NotImplementedException("currently only bitlength divideable by 8 are supported");
            }
            this.bitlength = bitlength;
            Signed = signed;
            this.data = value;
        }

        public static byte[] ApplyBitmask(byte[] data, byte[] bitmask)
        {
            int bytecount = Math.Min(data.Length, bitmask.Length);
            byte[] ret = new byte[bytecount];
            for (int i = 0; i < bytecount; i++)
            {
                ret[i] = (byte)(data[i] & bitmask[i]);
            }
            return ret;
        }

        public static byte[] GetBitMask(int bitlength)
        {
            byte[] mask = new byte[(int)Math.Ceiling((double)bitlength / 8.0)];
            int i = 0;
            for (i = 0; i < bitlength / 8; i++) //Fill all bytes that are full with ones
            {
                mask[i] = 0xFF;
            }
            if (i < mask.Length)
            {
                mask[i] = 0;
                for (int d = 0; d < bitlength % 8; d++)
                {
                    mask[i] |= (byte)(1 << d);
                }
            }
            return mask;
        }

        public bool Signed { get; protected set; }

        public override object InternalData => data;

        public override int ByteLength => bitlength / 8;

        public override int GetBytes(Span<byte> data)
        {
            if (this.data.TryWriteBytes(data, out int writtenBytes, !Signed))
            {
                return writtenBytes;
            }
            return 0;
        }

        public override int ParseBytes(ReadOnlySpan<byte> data)
        {
            if (data.Length < ByteLength)
            {
                return 0;
            }
            var slice = data.Slice(0, ByteLength);
            this.data = new BigInteger(slice, !Signed);
            return ByteLength;
        }

        public override double GetTransformed()
        {
            if(HasOutputTransform)
            {
                return OutputTransform((double)data);
            }
            else
            {
                return (double)data;
            }
        }
    }
}
