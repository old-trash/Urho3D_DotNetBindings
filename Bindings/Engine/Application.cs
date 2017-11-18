using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

public class Application : Object
{
    protected VariantMap engineParameters_;
    
    void_function_void SetupHandle = null;
    void_function_void StartHandle = null;
    void_function_void StopHandle = null;    

    public Application(Context context) : base(ApplicationEXT_ApplicationEXT(context.NativeInstance), context)
    {
        SetupHandle = Setup;
        StartHandle = Start;
        StopHandle = Stop;
    
        engineParameters_ = new VariantMap(ApplicationEXT_GetEngineParametersPtr(NativeInstance));
        ApplicationEXT_SetCallback_Setup(NativeInstance, SetupHandle);
        ApplicationEXT_SetCallback_Start(NativeInstance, StartHandle);
        ApplicationEXT_SetCallback_Stop(NativeInstance, StopHandle);
    }
    
    [MethodImpl(MethodImplOptions.NoOptimization)]
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

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
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
