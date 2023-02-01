using System.Diagnostics.CodeAnalysis;

namespace VerifyTests.ZeroLog;

public static class RecordingLogger
{
    static AsyncLocal<ConcurrentQueue<LoggedMessage>?> local = new();

    public static void Start() =>
        local.Value = new();

    internal static void Add(LoggedMessage logEvent)
    {
        var tracker = local.Value;
        tracker?.Enqueue(logEvent);
    }

    public static IReadOnlyCollection<LoggedMessage> GetFinishRecording()
    {
        if (TryFinishRecording(out var entries))
        {
            return entries;
        }

        return Array.Empty<LoggedMessage>();
    }

    public static bool TryFinishRecording([NotNullWhen(true)] out IReadOnlyCollection<LoggedMessage>? entries)
    {
        entries = local.Value;
        local.Value = null;
        return entries != null;
    }
}