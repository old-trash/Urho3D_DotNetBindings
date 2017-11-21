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
        engineParameters_[EngineParameters.FrameLimiter] = new Variant(false);
        GC.Collect(); // FOR TEST ONLY
    }
    
    void HandleUpdate(IntPtr eventData)
    {
        VariantMap map = new VariantMap(eventData);
        //Console.WriteLine("UPDATE " + map["TimeStep"].Value.Float + "s");
        
        Input input = GetSubsystem<Input>();
        if (input.GetKeyPress(Keys.F2))
        {
            DebugHud debugHud = GetSubsystem<DebugHud>();
            debugHud.ToggleAll();
        }
        
        GC.Collect();
    }

    public override void Start()
    {
        Log.Write(LogLevel.Info, "!!!!!!!!!!!!!!!!!!!!! Start()");
        Input input = context_.GetSubsystem<Input>();
        //input.SetMouseMode(MouseMode.Free);
        input.SetMouseVisible(true);
        
        Engine engine = context_.GetSubsystem<Engine>();
        DebugHud debugHud = engine.CreateDebugHud();
        
        ResourceCache cache = GetSubsystem<ResourceCache>();
        XMLFile xmlFile = cache.GetResource<XMLFile>("UI/DefaultStyle.xml");
        
        debugHud.SetDefaultStyle(xmlFile);
        debugHud.ToggleAll();
        GC.Collect(); // FOR TEST ONLY
        
        CreateLogo();
    }

    public override void Stop()
    {
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!! Stop()");
    }
    
    void CreateLogo()
    {
        ResourceCache cache = GetSubsystem<ResourceCache>();
        Texture2D logoTexture = cache.GetResource<Texture2D>("Textures/FishBoneLogo.png");
        
        UI ui = GetSubsystem<UI>();
        Sprite logoSprite_ = ui.GetRoot().CreateChild<Sprite>();
        logoSprite_.SetTexture(logoTexture);
        
        logoSprite_.SetSize(256, 128);
    }
}
