using System;
using System.Runtime.InteropServices;

public enum CreateMode
{
    REPLICATED = 0,
    LOCAL = 1
}

public enum TransformSpace
{
    TS_LOCAL = 0,
    TS_PARENT,
    TS_WORLD
}

public class Node : Animatable
{
    public Node(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public Node(Context context) : this(Node_Node(context.NativeInstance), context) { }
    
    public T CreateComponent<T>(CreateMode mode = CreateMode.REPLICATED, uint id = 0)
    {
        StringHash type = new StringHash(typeof(T).Name);
        IntPtr nativeComponent = Node_CreateComponent(NativeInstance, type, mode, id);
        return (T)Activator.CreateInstance(typeof(T), nativeComponent, context_);
    }
    
    public T GetComponent<T>(bool recursive = false)
    {
        StringHash type = new StringHash(typeof(T).Name);
        IntPtr nativeComponent = Node_GetComponent(NativeInstance, type, recursive);
        return (T)Activator.CreateInstance(typeof(T), nativeComponent, context_);
    }
    
    public Node CreateChild(string name = "", CreateMode mode = CreateMode.REPLICATED, uint id = 0, bool temporary = false)
    {
        IntPtr nativeNode = Node_CreateChild(NativeInstance, name, mode, id, temporary);
        return new Node(nativeNode, context_);
    }
    
    public void SetScale(float scale)
    {
        Node_SetScale_float(NativeInstance, scale);
    }
    
    public void SetScale(Vector3 scale)
    {
        Node_SetScale_Vector3(NativeInstance, ref scale);
    }
    
    public void SetDirection(Vector3 direction)
    {
        Node_SetDirection(NativeInstance, ref direction);
    }
    
    public void SetPosition(Vector3 position)
    {
        Node_SetPosition(NativeInstance, ref position);
    }
    
    public void SetRotation(Quaternion rotation)
    {
        Node_SetRotation(NativeInstance, ref rotation);
    }
    
    public void Translate(Vector3 delta, TransformSpace space = TransformSpace.TS_LOCAL)
    {
        Node_Translate(NativeInstance, ref delta, space);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Node_CreateComponent(IntPtr nativeInstance, StringHash type, CreateMode mode = CreateMode.REPLICATED, uint id = 0);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Node_GetComponent(IntPtr nativeInstance, StringHash type, bool recursive = false);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Node_CreateChild(IntPtr nativeInstance, string name = "", CreateMode mode = CreateMode.REPLICATED, uint id = 0, bool temporary = false);

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetScale_float(IntPtr nativeInstance, float scale);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetScale_Vector3(IntPtr nativeInstance, ref Vector3 scale);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetDirection(IntPtr nativeInstance, ref Vector3 direction);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetPosition(IntPtr nativeInstance, ref Vector3 position);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetRotation(IntPtr nativeInstance, ref Quaternion rotation);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_Translate(IntPtr nativeInstance, ref Vector3 delta, TransformSpace space = TransformSpace.TS_LOCAL);
}
