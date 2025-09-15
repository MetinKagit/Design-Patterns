using System.Text;
using UnityEngine;

public sealed class MemoryLog : ILog
{
    readonly StringBuilder _buf = new();
    public void Write(string msg) => _buf.AppendLine($"[LOCATOR] {msg}");
    public string ReadAll() => _buf.ToString();
}