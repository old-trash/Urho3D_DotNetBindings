using System;
using System.Runtime.InteropServices;

public class ResourceCache : Object
{
    public ResourceCache(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }
    
    public T GetResource<T>(string name, bool sendEventOnFailure = true)
    {
        StringHash type = new StringHash(typeof(T).Name);
        IntPtr nativeResource = ResourceCache_GetResource(NativeInstance, type, name, sendEventOnFailure);
        return (T)Activator.CreateInstance(typeof(T), nativeResource, context_);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr ResourceCache_GetResource(IntPtr nativeInstance, StringHash type, string name, bool sendEventOnFailure = true);
}
