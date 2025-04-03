using libxcm.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libparse.Container
{
    public class Symbol : AbstractDataClass
    {
        private Dictionary<string, IDataClass> storedData = new();

        public override int ByteLength
        {
            get
            {
                int bytes = 0;
                foreach(var dataClass in storedData.Values)
                {
                    bytes += dataClass.ByteLength;
                }
                return bytes;
            }
        }

        public void AddDataClass(IDataClass dataClass)
        {
            storedData.Add(dataClass.Name, dataClass);
        }

        public override int GetBytes(Span<byte> data)
        {
            Span<byte> slice = data;
            int totalReadbytes = 0;
            foreach (var dataClass in storedData.Values)
            {
                int readBytes = dataClass.GetBytes(slice);
                if(readBytes <= 0)
                {
                    break;
                }
                totalReadbytes += readBytes;
                slice = slice.Slice(readBytes);
            }
            return totalReadbytes;
        }

        public override IEnumerator<KeyValuePair<string, IDataClass>> GetEnumerator()
        {
            return storedData.GetEnumerator();
        }

        public override int ParseBytes(ReadOnlySpan<byte> data)
        {
            ReadOnlySpan<byte> slice = data;
            int totalParsedBytes = 0;
            foreach (var dataClass in storedData.Values)
            {
                int parsedBytes = dataClass.ParseBytes(slice);
                if(parsedBytes <= 0)
                {
                    break;
                }
                totalParsedBytes += parsedBytes;
                slice = data.Slice(parsedBytes);
            }
            return totalParsedBytes;
        }
    }
}
