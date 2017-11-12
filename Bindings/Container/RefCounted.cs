using System;
using System.Runtime.InteropServices;

abstract public class RefCounted
{
    internal IntPtr NativeInstance { get; private set; }
    
    protected RefCounted(IntPtr nativeInstance)
    {
        NativeInstance = nativeInstance;
        RefCounted_AddRef(NativeInstance);
    }
    
    ~RefCounted()
    {
        RefCounted_ReleaseRef(NativeInstance);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_AddRef(IntPtr nativeInstance);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_ReleaseRef(IntPtr nativeInstance);
}
