using System;
using System.Runtime.InteropServices;

public class Component : Animatable
{
    public Component(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Component(Context context) : this(Component_Component(context.NativeInstance), context) { }
}
