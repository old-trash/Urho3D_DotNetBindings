using System;
using System.Runtime.InteropServices;

static class MainClass
{
    static int Main()
    {
        Context context = new Context();
        MyApplication app = new MyApplication(context);
        return app.Run();
    }
}

class MyApplication : Application
{
    public MyApplication(Context context) : base(context)
    {
    }
}
