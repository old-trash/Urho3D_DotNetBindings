//
// Copyright (c) 2008-2017 the Urho3D project.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Runtime.InteropServices;
using System.Reflection;

/// Sample class, as framework for all samples.
///    - Initialization of the Urho3D engine (in Application class)
///    - Modify engine parameters for windowed mode and to show the class name as title
///    - Create Urho3D logo at screen
///    - Set custom window title and icon
///    - Create Console and Debug HUD, and use F1 and F2 key to toggle them
///    - Toggle rendering options from the keys 1-8
///    - Take screenshot with key 9
///    - Handle Esc key down to hide Console or exit application
///    - Init touch input on mobile platform using screen joysticks (patched for each individual sample)
class Sample : Application
{
    protected const float TOUCH_SENSITIVITY = 2.0f;


    public Sample(Context context) : base(context)
    {
       // SubscribeToEvent("Update", HandleUpdate);
    }

    public override void Setup()
    {
        // Modify engine startup parameters
        engineParameters_[EngineParameters.EP_WINDOW_TITLE] = new Variant(GetTypeName());
        engineParameters_[EngineParameters.EP_LOG_NAME]     = new Variant(GetSubsystem<FileSystem>().GetAppPreferencesDir("urho3d", "logs") + GetTypeName() + ".log");
        engineParameters_[EngineParameters.EP_FULL_SCREEN]  = new Variant(false);
        engineParameters_[EngineParameters.EP_HEADLESS]     = new Variant(false);
        engineParameters_[EngineParameters.EP_SOUND]        = new Variant(false);
        
        engineParameters_[EngineParameters.EP_FRAME_LIMITER]        = new Variant(false);

        // Construct a search path to find the resource prefix with two entries:
        // The first entry is an empty path which will be substituted with program/bin directory -- this entry is for binary when it is still in build tree
        // The second and third entries are possible relative paths from the installed program/bin directory to the asset directory -- these entries are for binary when it is in the Urho3D SDK installation location
        //if (!engineParameters_.Contains(EP_RESOURCE_PREFIX_PATHS))
        //    engineParameters_[EP_RESOURCE_PREFIX_PATHS] = new Variant(";../share/Resources;../share/Urho3D/Resources");

        GC.Collect(); // FOR TEST ONLY
    }
    
    /*void HandleUpdate(StringHash eventType, IntPtr eventData)
    {
        VariantMap map = new VariantMap(eventData);
        Console.WriteLine(eventType.Value == StringHash.Calculate("Update"));
        Console.WriteLine("UPDATE " + map["TimeStep"].Value.Float + "s");
        
        Input input = GetSubsystem<Input>();
        if (input.GetKeyPress(Keys.F2))
        {
            DebugHud debugHud = GetSubsystem<DebugHud>();
            debugHud.ToggleAll();
        }
        
        //GC.Collect();
    }*/

    public override void Start()
    {
        Log.Write(LogLevel.LOG_INFO, "!!!!!!!!!!!!!!!!!!!!! Start()");
        Input input = context_.GetSubsystem<Input>();
        //input.SetMouseMode(MouseMode.Free);
        //input.SetMouseVisible(true);
        
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
    
    protected Sprite logoSprite_;
    
    protected Scene scene_;
    /// Camera scene node.
    protected Node cameraNode_;
    /// Camera yaw angle.
    protected float yaw_;
    /// Camera pitch angle.
    protected float pitch_;
    
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
        logoSprite_.SetAlignment(HorizontalAlignment.HA_RIGHT, VerticalAlignment.VA_BOTTOM);

        // Make logo not fully opaque to show the scene underneath
        logoSprite_.SetOpacity(0.9f);

        // Set a low priority for the logo so that other UI elements can be drawn on top
        logoSprite_.SetPriority(-100);
    }
}
