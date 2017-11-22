using System;
using System.Runtime.InteropServices;
using System.Reflection;

public class Context : RefCounted
{
    public Context(IntPtr nativeInstance) : base(nativeInstance)
    {
    }

    public Context() : this(Context_Context()) { }
    
    public T GetSubsystem<T>()
    {
        StringHash type = new StringHash(typeof(T).Name);
        IntPtr nativeSubsystem = Context_GetSubsystem(NativeInstance, type);
        return (T)Activator.CreateInstance(typeof(T), nativeSubsystem, this);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Context_Context();
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Context_GetSubsystem(IntPtr nativeInstance, StringHash type);
}
