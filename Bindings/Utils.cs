using System;
using System.Runtime.InteropServices;

public static class Utils
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="Utils_FreeCString")]
    public static extern void FreeCString(IntPtr pointer);
}
