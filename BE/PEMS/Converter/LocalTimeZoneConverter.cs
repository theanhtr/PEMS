using System.Text.Json;
using System.Text.Json.Serialization;

namespace PEMS
{
    public class LocalTimeZoneConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valueStr = reader.GetString();
            var valueStrArray = valueStr?.Split("/");
            
            if (valueStrArray?.Length == 3 && valueStr?.Length <= 12)
            {
                var readerStr = valueStrArray[1] + "/" + valueStrArray[0] + "/" + valueStrArray[2];
                return DateTime.Parse(readerStr);
            }
            else
            {
                return DateTime.Parse(valueStr);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.Kind == DateTimeKind.Unspecified)
            {
                writer.WriteStringValue(DateTime.SpecifyKind(value, DateTimeKind.Local));
            } 
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
