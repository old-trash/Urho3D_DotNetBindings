using System;
using System.Runtime.InteropServices;

public enum CreateMode
{
    REPLICATED = 0,
    LOCAL = 1
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
    
    public Node CreateChild(string name = "", CreateMode mode = CreateMode.REPLICATED, uint id = 0, bool temporary = false)
    {
        IntPtr nativeNode = Node_CreateChild(NativeInstance, name, mode, id, temporary);
        return new Node(nativeNode, context_);
    }
    
    public void SetScale(ref Vector3 scale)
    {
        Node_SetScale(NativeInstance, ref scale);
    }
    
    public void SetDirection(ref Vector3 direction)
    {
        Node_SetDirection(NativeInstance, ref direction);
    }
    
    public void SetPosition(ref Vector3 position)
    {
        Node_SetPosition(NativeInstance, ref position);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Node_CreateComponent(IntPtr nativeInstance, StringHash type, CreateMode mode = CreateMode.REPLICATED, uint id = 0);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Node_CreateChild(IntPtr nativeInstance, string name = "", CreateMode mode = CreateMode.REPLICATED, uint id = 0, bool temporary = false);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetScale(IntPtr nativeInstance, ref Vector3 scale);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetDirection(IntPtr nativeInstance, ref Vector3 direction);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Node_SetPosition(IntPtr nativeInstance, ref Vector3 position);
}
