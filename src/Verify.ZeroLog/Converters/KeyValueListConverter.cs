using System.Globalization;

class KeyValueListConverter :
    WriteOnlyJsonConverter<KeyValueList>
{
    public override void Write(VerifyJsonWriter writer, KeyValueList list)
    {
        writer.WriteStartObject();
        foreach (var value in list)
        {
            writer.WriteMember(list, GetValue(value.Value), value.Key);
        }
        writer.WriteEndObject();
    }

    static object GetValue(ReadOnlySpan<char> span)
    {
        if (DateTime.TryParseExact(span, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out var dateTime))
        {
            return dateTime;
        }

        if (DateOnly.TryParseExact(span, "yyyy-MM-dd", null, DateTimeStyles.None, out var date))
        {
            return date;
        }

        if (TimeSpan.TryParseExact(span, @"hh\:mm\:ss\.fffffff", null, out var timeSpan))
        {
            return timeSpan;
        }

        if (TimeOnly.TryParseExact(span, @"HH\:mm\:ss\.fffffff", out var time))
        {
            return time;
        }

        return span.ToString();
    }
}