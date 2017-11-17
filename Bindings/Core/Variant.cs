using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct VariantStorage
{
    public UIntPtr Storage0;
    public UIntPtr Storage1;
    public UIntPtr Storage2;
    public UIntPtr Storage3;
}

[StructLayout(LayoutKind.Explicit)]
public struct VariantValue
{
    [FieldOffset(0)]
    public int Int;

    [FieldOffset(0)]
    public bool Bool;
    
    [FieldOffset(0)]
    public float Float;

    [FieldOffset(0)]
    public VariantStorage Storage; // Should be last??????
}

public enum VariantType
{
    None = 0,
    Int,
    Bool,
    Float,
    Vector2,
    Vector3,
    Vector4,
    Quaternion,
    Color,
    String,
    Buffer,
    VoidPtr,
    ResourceRef,
    ResourceRefList,
    VariantVector,
    VariantMap,
    IntRect,
    IntVector2,
    Ptr,
    Matrix3,
    Matrix3x4,
    Matrix4,
    Double,
    StringVector,
    Rect,
    IntVector3,
    Int64,
    CustomHeap,
    CustomStack,
    MaxVarTypes
}

[StructLayout(LayoutKind.Explicit)]
public struct Variant
{
    [FieldOffset(0)]
    public VariantType Type;
    
    [FieldOffset(8)]
    public VariantValue Value;
    
    public Variant(int value)
    {
        Type = VariantType.Int;
        Value = new VariantValue();
        Value.Int = value;
    }

    public Variant(bool value)
    {
        Type = VariantType.Bool;
        Value = new VariantValue();
        Value.Bool = value;
    }
    
    public Variant(float value)
    {
        Type = VariantType.Float;
        Value = new VariantValue();
        Value.Float = value;
    }
}

public class VariantMap
{
    private IntPtr nativeInstance;
    
    public IntPtr NativeInstance
    {
        get { return nativeInstance; }
    }

    public VariantMap(IntPtr nativeInstance)
    {
        this.nativeInstance = nativeInstance;
    }
    
    /*public VariantMap() // WARNING: NO DESTRUCTOR
    {
        nativeInstance = VariantMap_VariantMap();
    }*/

    public Variant this[StringHash key]
    {
        get
        {
            Variant value;
            VariantMap_GetValue(nativeInstance, key, out value);
            return value;
        }
        
        set
        {
            VariantMap_SetValue(nativeInstance, key, ref value);
        }
    }

    public Variant this[string key]
    {
        get
        {
            return this[new StringHash(key)];
        }
        
        set
        {
            this[new StringHash(key)] = value;
        }
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern IntPtr VariantMap_VariantMap();
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void VariantMap_GetValue(IntPtr nativeInstance, StringHash key, out Variant value);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void VariantMap_SetValue(IntPtr nativeInstance, StringHash key, ref Variant value);
}
