using System;
using System.Runtime.InteropServices;

public class Text : UIElement
{
    private const float DEFAULT_FONT_SIZE = 12;

    public Text(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    public Text(Context context) : this(Text_Text(context.NativeInstance), context) { }
    
    public void SetText(string text)
    {
        Text_SetText(NativeInstance, text);
    }
    
    public bool SetFont(Font font, float size = DEFAULT_FONT_SIZE)
    {
        return Text_SetFont(NativeInstance, font.NativeInstance, size);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Text_Text(IntPtr nativeContext);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Text_SetText(IntPtr nativeInstance, string text);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool Text_SetFont(IntPtr nativeInstance, IntPtr font, float size = DEFAULT_FONT_SIZE);
}
