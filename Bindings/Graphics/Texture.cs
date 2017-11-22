using System;
using System.Runtime.InteropServices;

public class Texture : ResourceWithMetadata
{
    public Texture(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public int GetWidth()
    {
        return Texture_GetWidth(NativeInstance);
    }

    public int GetHeight()
    {
        return Texture_GetHeight(NativeInstance);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern int Texture_GetWidth(IntPtr nativeInstance);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern int Texture_GetHeight(IntPtr nativeInstance);
}
