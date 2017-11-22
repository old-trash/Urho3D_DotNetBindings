using System;
using System.Runtime.InteropServices;

public class Camera : Component
{
    public Camera(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Camera(Context context) : this(Camera_Camera(context.NativeInstance), context) { }
}
