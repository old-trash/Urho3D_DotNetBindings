using System;
using System.Runtime.InteropServices;

public class Font : Resource
{
    public Font(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Font(Context context) : this(Font_Font(context.NativeInstance), context) { }
}
