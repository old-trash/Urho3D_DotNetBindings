using System;
using System.Runtime.InteropServices;

public enum MouseMode
{
    Absolute = 0,
    Relative,
    Wrap,
    Free,
    Invalid
}

public class Input : Object
{
    public Input(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public void SetMouseMode(MouseMode mode, bool suppressEvent = false)
    {
        Input_SetMouseMode(NativeInstance, mode, suppressEvent);
    }
    
    public void SetMouseVisible(bool enable, bool suppressEvent = false)
    {
        Input_SetMouseVisible(NativeInstance, enable, suppressEvent);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Input_SetMouseMode(IntPtr nativeInstance, MouseMode mode, bool suppressEvent);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Input_SetMouseVisible(IntPtr nativeInstance, bool enable, bool suppressEvent);
}
