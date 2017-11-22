using System;
using System.Runtime.InteropServices;

public class Zone : Drawable
{
    public Zone(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Zone(Context context) : this(Zone_Zone(context.NativeInstance), context) { }
    
    public void SetFogColor(ref Color color)
    {
        Zone_SetFogColor(NativeInstance, ref color);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Zone_SetFogColor(IntPtr nativeInstance, ref Color color);
}
