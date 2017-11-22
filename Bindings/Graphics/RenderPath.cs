using System;
using System.Runtime.InteropServices;

public class RenderPath : RefCounted
{
    public RenderPath(IntPtr nativeInstance) : base(nativeInstance)
    {
    }

    // public RenderPath() : this(RenderPath_RenderPath()) { }
}
