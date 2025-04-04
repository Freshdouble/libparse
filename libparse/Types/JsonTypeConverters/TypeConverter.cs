using System;
using libxcm.Types;
using Newtonsoft.Json;

namespace libxcm.Types.JsonConverters;

public class TypeConverter : JsonConverter<IType>
{
    public override IType ReadJson(JsonReader reader, Type objectType, IType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, IType value, JsonSerializer serializer)
    {
        writer.WriteValue(value.InternalData);
    }
}