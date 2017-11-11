using System;
using System.Runtime.InteropServices;

public class Application
{
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    public static extern IntPtr Application_Application(IntPtr nativeContext);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    public static extern int Application_Run(IntPtr nativeInstance);
}
