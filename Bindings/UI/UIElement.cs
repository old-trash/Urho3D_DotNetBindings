using System;
using System.Runtime.InteropServices;

public enum HorizontalAlignment
{
    HA_LEFT = 0,
    HA_CENTER,
    HA_RIGHT,
    HA_CUSTOM
}

public enum VerticalAlignment
{
    VA_TOP = 0,
    VA_CENTER,
    VA_BOTTOM,
    VA_CUSTOM
}

public class UIElement : Animatable
{
    public UIElement(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public T CreateChild<T>(string name = "", uint index = MathConsts.M_MAX_UNSIGNED)
    {
        StringHash type = new StringHash(typeof(T).Name);
        IntPtr nativeChild = UIElement_CreateChild(NativeInstance, type, name, index);
        return (T)Activator.CreateInstance(typeof(T), nativeChild, context_);
    }
    
    public void SetPriority(int priority)
    {
        UIElement_SetPriority(NativeInstance, priority);
    }
    
    public void SetOpacity(float opacity)
    {
        UIElement_SetOpacity(NativeInstance, opacity);
    }
    
    public void SetAlignment(HorizontalAlignment hAlign, VerticalAlignment vAlign)
    {
        UIElement_SetAlignment(NativeInstance, hAlign, vAlign);
    }
    
    public void SetSize(int width, int height)
    {
        UIElement_SetSize(NativeInstance, width, height);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr UIElement_CreateChild(IntPtr nativeInstance, StringHash type, string name = "", uint index = MathConsts.M_MAX_UNSIGNED);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UIElement_SetPriority(IntPtr nativeInstance, int priority);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UIElement_SetOpacity(IntPtr nativeInstance, float opacity);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UIElement_SetAlignment(IntPtr nativeInstance, HorizontalAlignment hAlign, VerticalAlignment vAlign);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void UIElement_SetSize(IntPtr nativeInstance, int width, int height);
}
