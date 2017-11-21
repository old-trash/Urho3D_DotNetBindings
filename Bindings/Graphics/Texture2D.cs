using System;
using System.Runtime.InteropServices;

public class Texture2D : Texture
{
    public Texture2D(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}
