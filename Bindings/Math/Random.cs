using System;
using System.Runtime.InteropServices;

public static partial class Math
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Rand();
}
