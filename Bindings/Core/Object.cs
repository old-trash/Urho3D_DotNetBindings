using System;
using System.Runtime.InteropServices;

public abstract class Object : RefCounted
{
    public Object(IntPtr nativeInstance) : base(nativeInstance)
    {
    }
}
