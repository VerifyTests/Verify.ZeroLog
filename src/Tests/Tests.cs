using VerifyTests.ZeroLog;
using ZeroLog;

[UsesVerify]
public class Tests
{
    #region usage

    [Fact]
    public Task Usage()
    {
        RecordingLogger.Start();
        var result = Method();

        return Verify(result);
    }

    static string Method()
    {
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The error");
        logger.Warn("The warning");
        return "Result";
    }

    #endregion

    [Fact]
    public Task Empty()
    {
        RecordingLogger.Start();
        return Verify("Result");
    }

    [Fact]
    public Task Single()
    {
        RecordingLogger.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message");
        return Verify(RecordingLogger.GetFinishRecording());
    }

    [Fact]
    public Task SingleNested()
    {
        RecordingLogger.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message");
        return Verify("Value");
    }

    [Fact]
    public Task Multiple()
    {
        RecordingLogger.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message1");
        logger.Error("The Message2");
        return Verify(RecordingLogger.GetFinishRecording());
    }

    [Fact]
    public Task MultipleNested()
    {
        RecordingLogger.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message1");
        logger.Error("The Message2");
        return Verify("Value");
    }

    [Fact]
    public Task AppendKeyValue()
    {
        RecordingLogger.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("key", "value")
            .Append("The Message")
            .Log();

        return Verify(RecordingLogger.GetFinishRecording());
    }

    [Fact]
    public Task ScrubbedKey()
    {
        RecordingLogger.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("key1", "value")
            .AppendKeyValue("key2", "value")
            .Append("The Message")
            .Log();

        return Verify(RecordingLogger.GetFinishRecording())
            .ScrubMember("key1");
    }

    [Fact]
    public Task DateValues()
    {
        RecordingLogger.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("Date", new DateOnly(2000, 1, 1))
            .AppendKeyValue("DateTime", DateTime.Now)
            //.AppendKeyValue("DateTimeOffset", DateTimeOffset.Now)
            .Append("The Message")
            .Log();

        return Verify(RecordingLogger.GetFinishRecording());
    }

    [Fact]
    public Task GuidValues()
    {
        RecordingLogger.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("Guid", Guid.NewGuid())
            .Append("The Message")
            .Log();

        return Verify(RecordingLogger.GetFinishRecording());
    }
}