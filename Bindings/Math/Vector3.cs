using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Vector3
{
    public float x_;
    public float y_;
    public float z_;

    public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        x_ = x;
        y_ = y;
        z_ = z;
    }
    
    public static Vector3 operator *(Vector3 v, float rhs) { return new Vector3(v.x_ * rhs, v.y_ * rhs, v.z_ * rhs); }

    public static readonly Vector3 ZERO = new Vector3(0.0f, 0.0f, 0.0f);
    public static readonly Vector3 LEFT = new Vector3(-1.0f, 0.0f, 0.0f);
    public static readonly Vector3 RIGHT = new Vector3(1.0f, 0.0f, 0.0f);
    public static readonly Vector3 UP = new Vector3(0.0f, 1.0f, 0.0f);
    public static readonly Vector3 DOWN = new Vector3(0.0f, -1.0f, 0.0f);
    public static readonly Vector3 FORWARD = new Vector3(0.0f, 0.0f, 1.0f);
    public static readonly Vector3 BACK = new Vector3(0.0f, 0.0f, -1.0f);
    public static readonly Vector3 ONE = new Vector3(1.0f, 1.0f, 1.0f);
}
