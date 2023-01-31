class KeyValueListConverter :
    WriteOnlyJsonConverter<KeyValueList>
{
    public override void Write(VerifyJsonWriter writer, KeyValueList list)
    {
        writer.WriteStartObject();
        foreach (var value in list)
        {
            writer.WriteMember(list, value.Value.ToString(), value.Key);
        }
        writer.WriteEndObject();
    }
}