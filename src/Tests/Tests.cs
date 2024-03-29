public class Tests
{
    #region Usage

    [Fact]
    public Task Usage()
    {
        Recording.Start();
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
        Recording.Start();
        return Verify("Result");
    }

    [Fact]
    public Task Single()
    {
        Recording.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message");
        return Verify(Recording.Stop());
    }

    [Fact]
    public Task SingleNested()
    {
        Recording.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message");
        return Verify("Value");
    }

    [Fact]
    public Task Multiple()
    {
        Recording.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message1");
        logger.Error("The Message2");
        return Verify();
    }

    [Fact]
    public Task MultipleNested()
    {
        Recording.Start();
        var logger = LogManager.GetLogger<Tests>();
        logger.Error("The Message1");
        logger.Error("The Message2");
        return Verify();
    }

    [Fact]
    public Task AppendKeyValue()
    {
        Recording.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("key", "value")
            .Append("The Message")
            .Log();

        return Verify();
    }

    [Fact]
    public Task ScrubbedKey()
    {
        Recording.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("key1", "value")
            .AppendKeyValue("key2", "value")
            .Append("The Message")
            .Log();

        return Verify(Recording.Stop())
            .ScrubMember("key1");
    }

    [Fact]
    public Task DateValues()
    {
        Recording.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("Date", new Date(2000, 1, 1))
            .AppendKeyValue("DateTime", DateTime.Now)
            .AppendKeyValue("DateTimeOffset", DateTimeOffset.Now)
            .Append("The Message")
            .Log();

        return Verify();
    }

    [Fact]
    public Task GuidValues()
    {
        Recording.Start();

        var logger = LogManager.GetLogger<Tests>();
        logger.Error()
            .AppendKeyValue("Guid", Guid.NewGuid())
            .Append("The Message")
            .Log();

        return Verify();
    }
}