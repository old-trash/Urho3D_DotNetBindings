using System;
using System.Runtime.InteropServices;

public enum LogLevel
{
    LOG_RAW = -1,
    LOG_TRACE = 0,
    LOG_DEBUG = 1,
    LOG_INFO = 2,
    LOG_WARNING = 3,
    LOG_ERROR = 4,
    LOG_NONE = 5
}

public static class Log
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="Log_Write")] 
    public static extern void Write(LogLevel level, string message);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="Log_WriteRaw")] 
    public static extern void WriteRaw(string message, bool error = false);
}
