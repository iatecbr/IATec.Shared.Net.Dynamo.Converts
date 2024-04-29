using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace IATec.Shared.Domain.Converts;

public class JsonObjectConverter : IPropertyConverter
{
    public DynamoDBEntry ToEntry(object value)
    {
        var json = JsonSerializer.Serialize(value);
        return new Primitive { Value = json };
    }

    public object FromEntry(DynamoDBEntry entry)
    {
        var json = entry.AsPrimitive().Value.ToString();

        if (json == null)
            return new { };

        return
            JsonSerializer.Deserialize(json,
                typeof(object)) ?? new { };
    }
}