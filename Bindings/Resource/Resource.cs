using System;
using System.Runtime.InteropServices;

public class Resource : Object
{
    public Resource(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}

public class ResourceWithMetadata : Resource
{
    public ResourceWithMetadata(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}
