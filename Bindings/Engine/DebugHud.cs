using System;
using System.Runtime.InteropServices;

public class DebugHud : Object
{
    public DebugHud(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    // public DebugHud(Context context) : this(DebugHud_DebugHud(context.NativeInstance), context) { }
    
    public void ToggleAll()
    {
        DebugHud_ToggleAll(NativeInstance);
    }
    
    public void SetDefaultStyle(XMLFile style)
    {
        DebugHud_SetDefaultStyle(NativeInstance, style.NativeInstance);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void DebugHud_ToggleAll(IntPtr nativeInstance);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void DebugHud_SetDefaultStyle(IntPtr nativeInstance, IntPtr style);
}
