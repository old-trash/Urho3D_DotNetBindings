using System;
using System.Runtime.InteropServices;

public class StaticModel : Drawable
{
    public StaticModel(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public StaticModel(Context context) : this(StaticModel_StaticModel(context.NativeInstance), context) { }
    
    public void SetModel(Model model)
    {
        StaticModel_SetModel(NativeInstance, model.NativeInstance);
    }
    
    public void SetMaterial(Material material)
    {
        StaticModel_SetMaterial(NativeInstance, material.NativeInstance);
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void StaticModel_SetModel(IntPtr nativeInstance, IntPtr model);
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern void StaticModel_SetMaterial(IntPtr nativeInstance, IntPtr material);
}
