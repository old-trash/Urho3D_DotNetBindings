using System;
using System.Runtime.InteropServices;

public class Font : Resource
{
    public Font(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}
