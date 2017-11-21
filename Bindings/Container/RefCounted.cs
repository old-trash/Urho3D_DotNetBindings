using System;
using System.Runtime.InteropServices;

public abstract class RefCounted : IDisposable
{
    public IntPtr NativeInstance { get; private set; }
    
    protected RefCounted(IntPtr nativeInstance)
    {
        NativeInstance = nativeInstance;
        if (NativeInstance != IntPtr.Zero)
            RefCounted_AddRef(NativeInstance);
    }
    
    ~RefCounted()
    {
        Dispose(false);
    }
    
    public bool IsNull()
    {
        return NativeInstance == IntPtr.Zero;
    }
    
    public static bool operator !(RefCounted r)
    {
        return r.IsNull();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private bool disposed = false;
    
    protected virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
            }

            if (NativeInstance != IntPtr.Zero)
                RefCounted_ReleaseRef(NativeInstance);
            disposed = true;
        }
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_AddRef(IntPtr nativeInstance);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_ReleaseRef(IntPtr nativeInstance);
}
