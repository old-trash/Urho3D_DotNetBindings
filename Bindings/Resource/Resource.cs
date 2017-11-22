using System;
using System.Runtime.InteropServices;

public class Resource : Object
{
    public Resource(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Resource(Context context) : this(Resource_Resource(context.NativeInstance), context) { }
}

public class ResourceWithMetadata : Resource
{
    public ResourceWithMetadata(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    // public ResourceWithMetadata(Context context) : this(ResourceWithMetadata_ResourceWithMetadata(context.NativeInstance), context) { }
}
