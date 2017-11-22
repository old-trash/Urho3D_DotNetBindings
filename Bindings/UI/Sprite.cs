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
    
    public void SetHotSpot(int x, int y)
    {
        Sprite_SetHotSpot(NativeInstance, x, y);
    }
    
    public void SetScale(float scale)
    {
        Sprite_SetScale(NativeInstance, scale);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Sprite_SetTexture(IntPtr nativeInstance, IntPtr texture);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Sprite_SetHotSpot(IntPtr nativeInstance, int x, int y);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Sprite_SetScale(IntPtr nativeInstance, float scale);
}
