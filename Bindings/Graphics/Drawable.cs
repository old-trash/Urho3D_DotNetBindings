using System;
using System.Runtime.InteropServices;

public class Drawable : Component
{
    public Drawable(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Drawable(Context context) : this(Drawable_Drawable(context.NativeInstance), context) { }
}
