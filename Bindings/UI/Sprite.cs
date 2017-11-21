using System;
using System.Runtime.InteropServices;

public class Sprite : UIElement
{
    public Sprite(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public void SetTexture(Texture texture)
    {
        Sprite_SetTexture(NativeInstance, texture.NativeInstance);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Sprite_SetTexture(IntPtr nativeInstance, IntPtr texture);
}
