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
        
        engineParameters_[EngineParameters.WindowWidth] = new Variant(800);
        engineParameters_[EngineParameters.WindowHeight] = new Variant(600);
        engineParameters_[EngineParameters.FullScreen] = new Variant(false);
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
