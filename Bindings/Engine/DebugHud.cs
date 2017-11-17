using System;
using System.Runtime.InteropServices;

public class DebugHud : Object
{
    public DebugHud(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public void ToggleAll()
    {
        DebugHud_ToggleAll(NativeInstance);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void DebugHud_ToggleAll(IntPtr nativeInstance);
}
