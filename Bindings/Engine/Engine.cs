using System;
using System.Runtime.InteropServices;

public class Engine : Object
{
    public Engine(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Engine(Context context) : this(Engine_Engine(context.NativeInstance), context) { }
    
    public DebugHud CreateDebugHud()
    {
        return new DebugHud(Engine_CreateDebugHud(NativeInstance), context_);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Engine_CreateDebugHud(IntPtr nativeInstance);
}
