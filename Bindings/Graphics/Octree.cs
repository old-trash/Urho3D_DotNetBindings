using System;
using System.Runtime.InteropServices;

public class Octree : Component
{
    public Octree(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Octree(Context context) : this(Octree_Octree(context.NativeInstance), context) { }
}

