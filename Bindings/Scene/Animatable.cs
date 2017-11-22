using System;
using System.Runtime.InteropServices;

public class Animatable : Serializable
{
    public Animatable(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Animatable(Context context) : this(Animatable_Animatable(context.NativeInstance), context) { }
}
