class VerifyAppender : Appender
{
    public override void WriteMessage(LoggedMessage message) =>
        Recording.Add("log", message.Clone());
}