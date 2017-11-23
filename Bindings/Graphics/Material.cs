using System;
using System.Runtime.InteropServices;

public class Material : Resource
{
    public Material(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    // public Material(Context context) : this(Material_Material(context.NativeInstance), context) { }
}
