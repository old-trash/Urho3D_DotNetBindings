using System;
using System.Runtime.InteropServices;

public class Engine : Object
{
    public Engine(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public void CreateDebugHud()
    {
        Engine_CreateDebugHud(NativeInstance);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Engine_CreateDebugHud(IntPtr nativeInstance);
}
