using UnityEngine;
using System.Text;
using System.IO;
using System;

public class SingletonLogger : Singleton<SingletonLogger>
{
    readonly StringBuilder _buf = new();

    public void Write(string msg)
    {
        _buf.AppendLine($"[SINGLETON] {msg}");
    }

    public string ReadAll() => _buf.ToString();
}
