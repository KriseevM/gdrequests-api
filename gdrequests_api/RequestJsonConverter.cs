using System.Text.Json;
using System.Text.Json.Serialization;
using gdrequests_api.Data.Entities;

namespace gdrequests_api;

public class RequestJsonConverter : JsonConverter<Request>
{   
    
    public override Request? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // We are not reading requests here. Only writing
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, Request value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("levelId", value.LevelId);
        writer.WriteNumber("addedAt", value.AddedAt);
        writer.WriteStartObject("metadata");
        writer.WriteNumber("difficulty", value.Metadata.Difficulty);
        writer.WriteNumber("rate", value.Metadata.Rate);
        writer.WriteNumber("authorId", value.Metadata.AuthorId);
        writer.WriteBoolean("isEpic", value.Metadata.IsEpic);
        writer.WriteBoolean("isFeatured", value.Metadata.IsFeatured);
        writer.WriteBoolean("isAuto", value.Metadata.IsAuto);
        writer.WriteBoolean("isDemon", value.Metadata.IsDemon);
        writer.WriteString("authorName", value.Metadata.Author);
        writer.WriteString("levelName", value.Metadata.LevelName);
        writer.WriteEndObject();
        writer.WriteEndObject();
    }
}