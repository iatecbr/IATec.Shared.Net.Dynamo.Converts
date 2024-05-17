using System.Collections.Concurrent;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace IATec.Shared.Net.Dynamo.Converts.Converts;

public class DictionaryConverter : IPropertyConverter
{
    public DynamoDBEntry ToEntry(object value)
    {
        var document = new Document();

        if (value is not ConcurrentDictionary<string, string> myDict)
            return document;

        foreach (var kvp in myDict)
        {
            document[kvp.Key] = kvp.Value;
        }

        return document;
    }

    public object FromEntry(DynamoDBEntry entry)
    {
        var document = entry as Document;

        var myDict = new ConcurrentDictionary<string, string>();

        if (document == null)
            return myDict;

        foreach (var kvp in document)
        {
            myDict.TryAdd(kvp.Key, kvp.Value.AsString());
        }

        return myDict;
    }
}