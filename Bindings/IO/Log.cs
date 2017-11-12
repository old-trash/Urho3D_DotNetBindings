using System;
using System.Runtime.InteropServices;

// FIX to match master
public enum LogLevel
{
    Raw = -1,
    Debug,
    Info,
    Warning,
    Error,
    None
}

public static class Log
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="Log_Write")] 
    public static extern void Write(LogLevel level, string message);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="Log_WriteRaw")] 
    public static extern void WriteRaw(string message, bool error = true);
}
