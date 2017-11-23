using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct IntVector2
{
    public int x_;
    public int y_;

    public IntVector2(int x = 0, int y = 0)
    {
        x_ = x;
        y_ = y;
    }
}
