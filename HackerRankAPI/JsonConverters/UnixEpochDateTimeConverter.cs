using System.Text.Json.Serialization;
using System.Text.Json;

namespace HackerRankAPI.JsonConverters
{
    sealed class UnixEpochDateTimeConverter : JsonConverter<DateTime>
    {
        static readonly DateTime s_epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int time = reader.GetInt32()!;

            return s_epoch.AddSeconds(time);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
