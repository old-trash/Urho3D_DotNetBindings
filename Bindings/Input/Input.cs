using System;
using System.Runtime.InteropServices;

public enum MouseMode
{
    MM_ABSOLUTE = 0,
    MM_RELATIVE,
    MM_WRAP,
    MM_FREE,
    MM_INVALID
}

public class Input : Object
{
    public Input(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Input(Context context) : this(Input_Input(context.NativeInstance), context) { }
    
    public void SetMouseMode(MouseMode mode, bool suppressEvent = false)
    {
        Input_SetMouseMode(NativeInstance, mode, suppressEvent);
    }
    
    public bool IsMouseVisible()
    {
        return Input_IsMouseVisible(NativeInstance);
    }
    
    public void SetMouseVisible(bool enable, bool suppressEvent = false)
    {
        Input_SetMouseVisible(NativeInstance, enable, suppressEvent);
    }
    
    public bool GetKeyDown(Keys key)
    {
        return Input_GetKeyDown(NativeInstance, (int)key);
    }

    public bool GetKeyPress(Keys key)
    {
        return Input_GetKeyPress(NativeInstance, (int)key);
    }
    
    public IntVector2 GetMouseMove()
    {
        IntVector2 result;
        Input_GetMouseMove(NativeInstance, out result);
        return result;
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Input_SetMouseMode(IntPtr nativeInstance, MouseMode mode, bool suppressEvent = false);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool Input_IsMouseVisible(IntPtr nativeInstance);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Input_SetMouseVisible(IntPtr nativeInstance, bool enable, bool suppressEvent = false);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool Input_GetKeyDown(IntPtr nativeInstance, int key);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool Input_GetKeyPress(IntPtr nativeInstance, int key);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Input_GetMouseMove(IntPtr nativeInstance, out IntVector2 result);
}
