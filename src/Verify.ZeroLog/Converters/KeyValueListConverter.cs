class KeyValueListConverter :
    WriteOnlyJsonConverter<KeyValueList>
{
    public override void Write(VerifyJsonWriter writer, KeyValueList list)
    {
        writer.WriteStartObject();
        foreach (var value in list)
        {
            writer.WriteMember(list, GetValue(value), value.Key);
        }

        writer.WriteEndObject();
    }

    static object GetValue(LoggedKeyValue value)
    {
        if (value.TryGetValue<DateTime>(out var dateTime))
        {
            return dateTime;
        }

        if (value.TryGetValue<DateTimeOffset>(out var dateTimeOffset))
        {
            return dateTimeOffset;
        }

        if (value.TryGetValue<Date>(out var date))
        {
            return date;
        }

        if (value.TryGetValue<TimeSpan>(out var timeSpan))
        {
            return timeSpan;
        }

        if (value.TryGetValue<Time>(out var time))
        {
            return time;
        }

        if (value.TryGetValue<Guid>(out var guid))
        {
            return guid;
        }

        return value.Value.ToString();
    }
}