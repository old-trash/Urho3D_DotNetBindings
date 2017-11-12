using System;
using System.Runtime.InteropServices;

static class MainClass
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
        MyApplication app = new MyApplication(new Context());
        return app.Run();
    }
}

class MyApplication : Application
{
    public MyApplication(Context context) : base(context)
    {
    }

    public override void Setup()
    {
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!! Setup()");
    }

    public override void Start()
    {
        Log.Write(LogLevel.Info, "!!!!!!!!!!!!!!!!!!!!! Start()");
    }

    public override void Stop()
    {
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!! Stop()");
    }
}
