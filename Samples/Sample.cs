using System;
using System.Runtime.InteropServices;
using System.Reflection;

class Sample : Application
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
        Sample app = new Sample(new Context());
        return app.Run();
    }

    public Sample(Context context) : base(context)
    {
        SubscribeToEvent(new StringHash("Update"), HandleUpdate);
    }

    public override void Setup()
    {
        //engineParameters_[EngineParameters.WindowTitle] = GetTypeName();
        //engineParameters_[EP_LOG_NAME]     = GetSubsystem<FileSystem>()->GetAppPreferencesDir("urho3d", "logs") + GetTypeName() + ".log";
        engineParameters_[EngineParameters.FullScreen]   = new Variant(false);
        engineParameters_[EngineParameters.Headless]     = new Variant(false);
        engineParameters_[EngineParameters.Sound]        = new Variant(false);
    }
    
    void HandleUpdate(IntPtr eventData)
    {
        VariantMap map = new VariantMap(eventData);
        Console.WriteLine("UPDATE " + map["TimeStep"].Value.Float + "s");
    }

    public override void Start()
    {
        Log.Write(LogLevel.Info, "!!!!!!!!!!!!!!!!!!!!! Start()");
        Input input = context_.GetSubsystem<Input>();
        //input.SetMouseMode(MouseMode.Free);
        input.SetMouseVisible(true);
       
    }

    public override void Stop()
    {
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!! Stop()");
    }
}
