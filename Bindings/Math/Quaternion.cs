using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Quaternion
{
    public float w_;
    public float x_;
    public float y_;
    public float z_;

    public Quaternion(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 1.0f)
    {
        x_ = x;
        y_ = y;
        z_ = z;
        w_ = w;
    }
    
    public static Quaternion FromEulerAngles(float x, float y, float z)
    {
        Quaternion result;
        Quaternion_FromEulerAngles(x, y, z, out result);
        return result;
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Quaternion_FromEulerAngles(float x, float y, float z, out Quaternion result);
}
