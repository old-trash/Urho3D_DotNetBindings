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
    public int int_;

    [FieldOffset(0)]
    public bool bool_;
    
    [FieldOffset(0)]
    public float float_;

    [FieldOffset(0)]
    public VariantStorage storage_; // Should be last??????
}

public enum VariantType
{
    VAR_NONE = 0,
    VAR_INT,
    VAR_BOOL,
    VAR_FLOAT,
    VAR_VECTOR2,
    VAR_VECTOR3,
    VAR_VECTOR4,
    VAR_QUATERNION,
    VAR_COLOR,
    VAR_STRING,
    VAR_BUFFER,
    VAR_VOIDPTR,
    VAR_RESOURCEREF,
    VAR_RESOURCEREFLIST,
    VAR_VARIANTVECTOR,
    VAR_VARIANTMAP,
    VAR_INTRECT,
    VAR_INTVECTOR2,
    VAR_PTR,
    VAR_MATRIX3,
    VAR_MATRIX3X4,
    VAR_MATRIX4,
    VAR_DOUBLE,
    VAR_STRINGVECTOR,
    VAR_RECT,
    VAR_INTVECTOR3,
    VAR_INT64,
    VAR_CUSTOM_HEAP,
    VAR_CUSTOM_STACK,
    MAX_VAR_TYPES
}

[StructLayout(LayoutKind.Explicit)]
public struct Variant
{
    [FieldOffset(0)]
    private VariantType type_;
    
    [FieldOffset(8)]
    public VariantValue value_;
    
    public Variant(int value)
    {
        type_ = VariantType.VAR_INT;
        value_ = new VariantValue();
        value_.int_ = value;
    }

    public int GetInt()
    {
        return value_.int_;
    }

    public Variant(bool value)
    {
        type_ = VariantType.VAR_BOOL;
        value_ = new VariantValue();
        value_.bool_ = value;
    }
    
    public bool GetBool()
    {
        return value_.bool_;
    }

    public Variant(float value)
    {
        type_ = VariantType.VAR_FLOAT;
        value_ = new VariantValue();
        value_.float_ = value;
    }

    public float GetFloat()
    {
        return value_.float_;
    }
    
    public Variant(string value)
    {
        type_ = VariantType.VAR_NONE;
        value_ = new VariantValue();
        Variant_SetCString(ref this, value);
    }
    
    public string GetString()
    {
        IntPtr nativeCString = Variant_GetCString(ref this);
        string result = Marshal.PtrToStringAnsi(nativeCString);
        Utils.FreeCString(nativeCString);
        return result;
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Variant_GetCString(ref Variant variant);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Variant_SetCString(ref Variant variant, string value);
}

public class VariantMap
{
    public IntPtr NativeInstance { get; private set; }

    public VariantMap(IntPtr nativeInstance)
    {
        NativeInstance = nativeInstance;
    }

    public Variant this[StringHash key]
    {
        get
        {
            Variant value;
            VariantMap_GetValue(NativeInstance, key, out value);
            return value;
        }
        
        set
        {
            VariantMap_SetValue(NativeInstance, key, ref value);
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
    private static extern void VariantMap_GetValue(IntPtr nativeInstance, StringHash key, out Variant value);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)] 
    private static extern void VariantMap_SetValue(IntPtr nativeInstance, StringHash key, ref Variant value);
}
