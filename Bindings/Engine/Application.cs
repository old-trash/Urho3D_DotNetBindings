using System;
using System.Runtime.InteropServices;

public class Application : Object
{
    public Application(Context context) : base(Application_Application(context.NativeInstance))
    {
    }
    
    public int Run()
    {
        return Application_Run(NativeInstance);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern IntPtr Application_Application(IntPtr nativeContext);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern int Application_Run(IntPtr nativeInstance);
}
