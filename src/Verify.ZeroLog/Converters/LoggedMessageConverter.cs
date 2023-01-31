class LoggedMessageConverter :
    WriteOnlyJsonConverter<LoggedMessage>
{
    public override void Write(VerifyJsonWriter writer, LoggedMessage message)
    {
        writer.WriteStartObject();
        writer.WriteMember(message, message.Message.ToString(), "Message");
        writer.WriteMember(message, message.Level, "Level");
        writer.WriteMember(message, message.LoggerName, "Logger");
        writer.WriteMember(message, message.KeyValues, "KeyValues");
        writer.WriteMember(message, message.Exception, "Exception");
        writer.WriteEndObject();
    }
}