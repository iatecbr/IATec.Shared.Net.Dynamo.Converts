using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace IATec.Shared.Net.Dynamo.Converts;

public class JsonObjectConverter<T> : IPropertyConverter
{
    public DynamoDBEntry ToEntry(object value)
    {
        var json = JsonSerializer.Serialize(value);
        return new Primitive { Value = json };
    }

    public object? FromEntry(DynamoDBEntry entry)
    {
        var json = entry.AsPrimitive().Value.ToString();

        if (json == null)
            return null;

        return
            JsonSerializer.Deserialize(json,
                typeof(T)) ?? null;
    }
}