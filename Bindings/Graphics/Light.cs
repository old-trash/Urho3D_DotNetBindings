using System;
using System.Runtime.InteropServices;

public enum LightType
{
    LIGHT_DIRECTIONAL = 0,
    LIGHT_SPOT,
    LIGHT_POINT
}

public class Light : Drawable
{
    public Light(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Light(Context context) : this(Light_Light(context.NativeInstance), context) { }
    
    public void SetLightType(LightType type)
    {
        Light_SetLightType(NativeInstance, type);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Light_SetLightType(IntPtr nativeInstance, LightType type);
}
