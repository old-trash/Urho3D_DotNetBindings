using System;
using System.Runtime.InteropServices;

public static partial class Math
{
    public const uint M_MAX_UNSIGNED = 0xffffffff;
    
    public static float Random()
    {
        return Random_void();
    }
    
    public static float Random(float range)
    {
        return Random_float(range);
    }
    
    public static float Clamp(float value, float min, float max)
    {
        if (value < min)
            return min;
        else if (value > max)
            return max;
        else
            return value;
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Random_void();

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float Random_float(float range);
}
