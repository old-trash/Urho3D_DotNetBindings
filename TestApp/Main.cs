using System;
using System.Runtime.InteropServices;

static class MainClass
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
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
    
    public override void Start()
    {
        Log.Write(LogLevel.Info, "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
