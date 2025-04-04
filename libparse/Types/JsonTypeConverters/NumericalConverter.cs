using System;
using libxcm.Types;
using Newtonsoft.Json;

namespace libxcm.Types.JsonConverters;

public class NumericalConverter : JsonConverter<AbstractNumericalType>
{
    public override AbstractNumericalType ReadJson(JsonReader reader, Type objectType, AbstractNumericalType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, AbstractNumericalType value, JsonSerializer serializer)
    {
        if(value.HasOutputTransform)
        {
            writer.WriteValue(value.GetTransformed());
        }
        else
        {
            writer.WriteValue(value.InternalData);
        }
    }
}