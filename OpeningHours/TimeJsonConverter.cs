using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpeningHours
{
    internal class TimeJsonConverter : JsonConverter<Time>
    {
        public override Time Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
            => reader.GetString();

        public override void Write(Utf8JsonWriter writer, Time value, JsonSerializerOptions options) 
            => writer.WriteStringValue($"{value.Hour}:{value.Minute}");
    }
}
