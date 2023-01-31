class VerifyAppender : Appender
{
    public override void WriteMessage(LoggedMessage message) =>
        RecordingLogger.Add(message);
}