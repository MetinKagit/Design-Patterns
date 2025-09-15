using System.Text;
public sealed class NullLog : ILog
{
    static readonly StringBuilder _buf = new();
    public void Write(string msg) { }
    public string ReadAll() => string.Empty;
}
