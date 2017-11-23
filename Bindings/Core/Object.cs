using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;


public class Object : RefCounted
{
    protected Context context_;
    
    // TODO: deleting
    List<void_function_StringHash_VariantMap> storage = new List<void_function_StringHash_VariantMap>();

    public Object(IntPtr nativeInstance, Context context) : base(nativeInstance)
    {
        context_ = context;
    }

    // public Object(Context context) : this(Object_Object(context.NativeInstance), context) { }
    
    protected void SubscribeToEvent(StringHash eventType, void_function_StringHash_VariantMap function)
    {
        storage.Add(function);
        Object_SubscribeToEvent(NativeInstance, eventType, function);
    }
    
    public T GetSubsystem<T>()
    {
        return context_.GetSubsystem<T>();
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void void_function_StringHash_VariantMap(StringHash eventType, IntPtr nativeVariantMap);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void Object_SubscribeToEvent(IntPtr nativeInstance, StringHash eventType, void_function_StringHash_VariantMap function);
}
