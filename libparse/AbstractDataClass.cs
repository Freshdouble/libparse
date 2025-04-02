using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libparse
{
    public abstract class AbstractDataClass : IDataClass
    {
        public string Name { get; set; }
        public int BitLength => ByteLength * 8;
        public abstract int ByteLength { get; }

        public byte[] GetBytes()
        {
            byte[] data = new byte[ByteLength];
            GetBytes(data);
            return data;
        }
        public abstract int GetBytes(Span<byte> data);
        public abstract IEnumerator<KeyValuePair<string, IDataClass>> GetEnumerator();
        public abstract int ParseBytes(ReadOnlySpan<byte> data);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
