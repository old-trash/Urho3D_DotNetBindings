using System;
using System.Runtime.InteropServices;

public class Scene : Node
{
    public Scene(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    public Scene(Context context) : this(Scene_Scene(context.NativeInstance), context) { }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Scene_Scene(IntPtr nativeContext);
}
