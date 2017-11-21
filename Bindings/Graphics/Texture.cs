using System;
using System.Runtime.InteropServices;

public class Texture : ResourceWithMetadata
{
    public Texture(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}
