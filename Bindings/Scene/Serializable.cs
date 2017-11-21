using System;
using System.Runtime.InteropServices;

public class Serializable : Object
{
    public Serializable(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
}
