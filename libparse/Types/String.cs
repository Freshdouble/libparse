using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace libxcm.Types
{
    public class String : AbstractType, IVariableLength
    {
        protected string data = string.Empty;
        private Encoding encoding = Encoding.ASCII; //Default Encoding

        public String(int length, bool isfixed = false)
        {
            if(isfixed)
            {
                Stringlength = length;
                IsFixedLength = true;
                data = FillStringWith(string.Empty, length, '\0');
            }
            MaxBitLength = length * 8;
        }

        public String(int length, Encoding encoding) : this(length) => this.encoding = encoding;

        public int Stringlength { get; protected set; } = 0;

        public int MaxBitLength { get; set; }

        public bool IsFixedLength { get; protected set; } = false;

        public override object InternalData => data;

        public override int ByteLength => data.Length;

        protected static string FillStringWith(string source, int length, char add)
        {
            while(length > 0)
            {
                source += add;
                length--;
            }
            return source;
        }

        public override string ToString() => data;

        public void SetValue(string data)
        {
            this.data = data;
            int maxlen = MaxBitLength / 8;
            if (maxlen > 0 && this.data.Length > maxlen)
            {
                this.data = this.data.Substring(0, maxlen);
            }
        }

        public override int GetBytes(Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public override int ParseBytes(ReadOnlySpan<byte> data)
        {
            int length = 0;
            if (IsFixedLength)
            {
                length = Stringlength;
            }
            else
            {
                for (int i = 0; i < Math.Min(data.Length, MaxBitLength / 8); i++)
                {
                    if (data[i] != 0)
                    {
                        length++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (data.Length >= length)
            {
                if (length > 0)
                {
                    var slice = data.Slice(0, length);
                    this.data = encoding.GetString(slice);
                }
                return length;
            }
            else
            {
                return 0;
            }
        }
    }
}
