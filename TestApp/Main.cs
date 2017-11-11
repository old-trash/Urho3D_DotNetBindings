using System;
using System.Runtime.InteropServices;

static class MainClass
{
    static void Main()
    {
        IntPtr nativeContext = Context.Context_Context();
        IntPtr nativeApplication = Application.Application_Application(nativeContext);
        Application.Application_Run(nativeApplication);
    }
}
