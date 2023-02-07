namespace VerifyTests;

public static class VerifyZeroLog
{
    public static bool Initialized { get; private set; }

    [Obsolete("Use Initialize()")]
    public static void Enable() =>
        Initialize();

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
        VerifierSettings.RegisterJsonAppender(
            _ =>
            {
                if (!RecordingLogger.TryFinishRecording(out var entries))
                {
                    return null;
                }

                if (!entries.Any())
                {
                    return null;
                }

                if (entries.Count ==1)
                {
                    return new("log", entries.Single());
                }

                return new("logs", entries);
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
