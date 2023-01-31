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
        logger.Error("The Message");
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
    public Task AppendKeyValue()
    {
        RecordingLogger.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("key", "value")
            .Append("The Message")
            .Log();

        return Verify("Result");
    }
}