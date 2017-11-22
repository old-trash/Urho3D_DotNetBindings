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
}
