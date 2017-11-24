using System;
using System.Runtime.InteropServices;

public class FileSystem : Object
{
    public FileSystem(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public FileSystem(Context context) : this(FileSystem_FileSystem(context.NativeInstance), context) { }
    
    public string GetAppPreferencesDir(string org, string app)
    {
        IntPtr nativeCString = FileSystem_GetAppPreferencesDir(NativeInstance, org, app);
        string result = Marshal.PtrToStringAnsi(nativeCString);
        Utils.FreeCString(nativeCString);
        return result;
    }
    
    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr FileSystem_GetAppPreferencesDir(IntPtr nativeInstance, string org, string app);
}
