using System;
using System.Runtime.InteropServices;

public class Application : Object
{
    protected VariantMap engineParameters_;

    public Application(Context context) : base(ApplicationEXT_ApplicationEXT(context.NativeInstance))
    {
        engineParameters_ = new VariantMap(ApplicationEXT_GetEngineParametersPtr(NativeInstance));
        ApplicationEXT_SetCallback_Setup(NativeInstance, Setup);
        ApplicationEXT_SetCallback_Start(NativeInstance, Start);
        ApplicationEXT_SetCallback_Stop(NativeInstance, Stop);
    }
    
    public int Run()
    {
        return ApplicationEXT_Run(NativeInstance);
    }
    
    public virtual void Setup()
    {
    }
    
    public virtual void Start()
    {
    }
    
    public virtual void Stop()
    {
    }
    
    private delegate void void_function_void();

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern IntPtr ApplicationEXT_ApplicationEXT(IntPtr nativeContext);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern int ApplicationEXT_Run(IntPtr nativeInstance);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void ApplicationEXT_SetCallback_Setup(IntPtr nativeInstance, void_function_void callback);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void ApplicationEXT_SetCallback_Start(IntPtr nativeInstance, void_function_void callback);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void ApplicationEXT_SetCallback_Stop(IntPtr nativeInstance, void_function_void callback);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern IntPtr ApplicationEXT_GetEngineParametersPtr(IntPtr nativeInstance);
}
