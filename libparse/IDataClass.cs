using libxcm.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libparse
{
    public interface IDataClass : IEnumerable<KeyValuePair<string, IDataClass>>
    {
        string Name { get; set; }
        int BitLength { get; }
        int ByteLength { get; }
        int ParseBytes(ReadOnlySpan<byte> data);

        byte[] GetBytes();
        int GetBytes(Span<byte> data);
    }
}
