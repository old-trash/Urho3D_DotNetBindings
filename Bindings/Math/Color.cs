using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Color
{
    public float r_;
    public float g_;
    public float b_;
    public float a_;

    public Color(float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f)
    {
        r_ = r;
        g_ = g;
        b_ = b;
        a_ = a;
    }
}
