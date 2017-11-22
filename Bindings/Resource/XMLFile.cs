using System;
using System.Runtime.InteropServices;

public class XMLFile : Resource
{
    public XMLFile(IntPtr nativeInstance, Context context) : base(nativeInstance, context)
    {
    }

    // public XMLFile(Context context) : this(XMLFile_XMLFile(context.NativeInstance), context) { }
}
