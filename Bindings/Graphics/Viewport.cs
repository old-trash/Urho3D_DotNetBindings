using System;
using System.Runtime.InteropServices;

public class Viewport : Object
{
    public Viewport(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Viewport(Context context) : this(Viewport_Viewport(context.NativeInstance), context) { }
    
    public Viewport(Context context, Scene scene, Camera camera, RenderPath renderPath)
        : this(Viewport_Viewport(context.NativeInstance, scene.NativeInstance, camera.NativeInstance, renderPath.NativeInstance), context) { }

    public Viewport(Context context, Scene scene, Camera camera)
        : this(Viewport_Viewport(context.NativeInstance, scene.NativeInstance, camera.NativeInstance, IntPtr.Zero), context) { }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Viewport_Viewport(IntPtr nativeContext, IntPtr scene, IntPtr camera, IntPtr renderPath);
}
