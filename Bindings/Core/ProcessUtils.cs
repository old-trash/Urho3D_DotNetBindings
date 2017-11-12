using System;
using System.Runtime.InteropServices;

public static class ProcessUtils
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="ProcessUtils_ParseArguments")]
    public static extern void ParseArguments(string cmdLine);
}
