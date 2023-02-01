class LoggedMessageConverter :
    WriteOnlyJsonConverter<LoggedMessage>
{
    public override void Write(VerifyJsonWriter writer, LoggedMessage message)
    {
        writer.WriteStartObject();
        writer.WritePropertyName(message.Level.ToString());
        writer.WriteValue(message.Message.ToString());
        writer.WriteMember(message, message.LoggerName, "Logger");
        if (message.KeyValues.Count != 0)
        {
            writer.WriteMember(message, message.KeyValues, "KeyValues");
        }
        writer.WriteMember(message, message.Exception, "Exception");
        writer.WriteEndObject();
    }
}
