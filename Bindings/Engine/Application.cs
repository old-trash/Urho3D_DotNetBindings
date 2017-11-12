using System;
using System.Runtime.InteropServices;

public class Application : Object
{
    public Application(Context context) : base(Application_Application(context.NativeInstance))
    {
        Application_SetCallback_Start(NativeInstance, Start);
    }
    
    public int Run()
    {
        return Application_Run(NativeInstance);
    }
    
    public virtual void Start()
    {
    }
    
    public delegate void void_function_void();

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern IntPtr Application_Application(IntPtr nativeContext);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern int Application_Run(IntPtr nativeInstance);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void Application_SetCallback_Start(IntPtr nativeInstance, void_function_void callback);
}
