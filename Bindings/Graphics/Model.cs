using System;
using System.Runtime.InteropServices;

public class Model : ResourceWithMetadata
{
    public Model(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    // public Model(Context context) : this(Model_Model(context.NativeInstance), context) { }
}
