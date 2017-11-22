using System;
using System.Runtime.InteropServices;
using System.Reflection;

class Sample : Application
{
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
        //GC.Collect(); // FOR TEST ONLY
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
        
        //GC.Collect();
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
        //GC.Collect(); // FOR TEST ONLY
        
        CreateLogo();
    }

    public override void Stop()
    {
        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!! Stop()");
    }
    
    Sprite logoSprite_;
    
    void CreateLogo()
    {
        // Get logo texture
        ResourceCache cache = GetSubsystem<ResourceCache>();
        Texture2D logoTexture = cache.GetResource<Texture2D>("Textures/FishBoneLogo.png");
        if (!logoTexture)
            return;
        
        // Create logo sprite and add to the UI layout
        UI ui = GetSubsystem<UI>();
        logoSprite_ = ui.GetRoot().CreateChild<Sprite>();
        
        // Set logo sprite texture
        logoSprite_.SetTexture(logoTexture);

        int textureWidth = logoTexture.GetWidth();
        int textureHeight = logoTexture.GetHeight();

        // Set logo sprite scale
        logoSprite_.SetScale(256.0f / textureWidth);

        // Set logo sprite size
        logoSprite_.SetSize(textureWidth, textureHeight);

        // Set logo sprite hot spot
        logoSprite_.SetHotSpot(textureWidth, textureHeight);

        // Set logo sprite alignment
        logoSprite_.SetAlignment(HorizontalAlignment.RIGHT, VerticalAlignment.BOTTOM);

        // Make logo not fully opaque to show the scene underneath
        logoSprite_.SetOpacity(0.9f);

        // Set a low priority for the logo so that other UI elements can be drawn on top
        logoSprite_.SetPriority(-100);
    }
}
