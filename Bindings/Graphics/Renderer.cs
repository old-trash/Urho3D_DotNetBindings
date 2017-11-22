using System;
using System.Runtime.InteropServices;

public class Renderer : Object
{
    public Renderer(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Renderer(Context context) : this(Renderer_Renderer(context.NativeInstance), context) { }
    
    public void SetViewport(uint index, Viewport viewport)
    {
        Renderer_SetViewport(NativeInstance, index, viewport.NativeInstance);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Renderer_SetViewport(IntPtr nativeInstance, uint index, IntPtr viewport);
}
