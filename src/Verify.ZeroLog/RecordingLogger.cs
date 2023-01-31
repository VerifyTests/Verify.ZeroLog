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

    public static bool TryFinishRecording([NotNullWhen(true)] out IEnumerable<LoggedMessage>? entries)
    {
        var events = local.Value;

        if (events is null)
        {
            local.Value = null;
            entries = null;
            return false;
        }

        entries = events.ToArray();
        local.Value = null;
        return true;
    }
}