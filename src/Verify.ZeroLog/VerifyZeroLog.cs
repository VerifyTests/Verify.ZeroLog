namespace VerifyTests;

public static class VerifyZeroLog
{
    public static void Enable()
    {
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

                return new("logs", entries);
            });

        LogManager.Initialize(new()
        {
            RootLogger =
            {
                Appenders =
                {
                    new VerifyAppender()
                }
            },
            UseBackgroundThread = false,
        });
    }
}