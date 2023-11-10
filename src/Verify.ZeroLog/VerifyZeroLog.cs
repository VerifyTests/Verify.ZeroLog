namespace VerifyTests;

public static class VerifyZeroLog
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ =>
        {
            _.Converters.Add(new KeyValueListConverter());
            _.Converters.Add(new LoggedMessageConverter());
        });

        if (LogManager.Configuration is { } config)
        {
            config.RootLogger.Appenders.Add(new VerifyAppender());
            config.ApplyChanges();
        }
        else
        {
            config = ZeroLogConfiguration.CreateTestConfiguration();
            config.RootLogger.Appenders.Add(new VerifyAppender());
            LogManager.Initialize(config);
        }
    }
}
