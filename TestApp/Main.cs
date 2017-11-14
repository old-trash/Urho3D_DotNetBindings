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
        
        
        VariantValue varVal = new VariantValue();
        varVal.Int = 500;
        
        Console.WriteLine("TEST VARVAL " + varVal.Int);
   
        engineParameters_[EngineParameters.WindowWidth] = new Variant(800);
        engineParameters_[EngineParameters.WindowHeight] = new Variant(600);
        engineParameters_[EngineParameters.FullScreen] = new Variant(false);
        
        Console.WriteLine("\nWINDOW WIDTH = " + engineParameters_["WindowWidth"].Value.Int);
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
