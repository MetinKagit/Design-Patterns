public static class Services
{
    static readonly ILog _nullLog = new NullLog();
    static ILog _log = _nullLog;

    public static void Provide(ILog log) => _log = log ?? _nullLog;

    public static ILog Log => _log;
}
