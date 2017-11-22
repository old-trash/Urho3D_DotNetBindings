using System;
using System.Runtime.InteropServices;

public class UI : Object
{
    public UI(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    // public UI(Context context) : this(UI_UI(context.NativeInstance), context) { }
    
    public UIElement GetRoot()
    {
        return new UIElement(UI_GetRoot(NativeInstance), context_);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr UI_GetRoot(IntPtr nativeInstance);
}
