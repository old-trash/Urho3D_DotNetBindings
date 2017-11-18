using System;
using System.Runtime.InteropServices;

public abstract class RefCounted : IDisposable
{
    internal IntPtr NativeInstance { get; private set; }
    
    protected RefCounted(IntPtr nativeInstance)
    {
        NativeInstance = nativeInstance;
        RefCounted_AddRef(NativeInstance);
    }
    
    ~RefCounted()
    {
        Dispose(false);
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

            RefCounted_ReleaseRef(NativeInstance);
            disposed = true;
        }
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_AddRef(IntPtr nativeInstance);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void RefCounted_ReleaseRef(IntPtr nativeInstance);
}
