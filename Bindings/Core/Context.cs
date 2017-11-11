using System;
using System.Runtime.InteropServices;

public class Context
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr Context_Context();
}
