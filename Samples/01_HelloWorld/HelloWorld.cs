using System;

class HelloWorld : Sample
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
        HelloWorld app = new HelloWorld(new Context());
        return app.Run();
    }

    public HelloWorld(Context context) : base(context)
    {
    }
    
    public override void Start()
    {
        // Execute base class startup
        base.Start();
        
        // Create "Hello World" Text
        CreateText();

        // Finally subscribe to the update event. Note that by subscribing events at this point we have already missed some events
        // like the ScreenMode event sent by the Graphics subsystem when opening the application window. To catch those as well we
        // could subscribe in the constructor instead.
        //SubscribeToEvents();

        // Set the mouse mode to use in the sample
        //Sample::InitMouseMode(MM_FREE);
    }
    
    void CreateText()
    {
        ResourceCache cache = GetSubsystem<ResourceCache>();

        // Construct new Text object
        Text helloText = new Text(context_);

        // Set String to display
        helloText.SetText("Hello World from Urho3D!");

        // Set font and text color
        helloText.SetFont(cache.GetResource<Font>("Fonts/Anonymous Pro.ttf"), 30);
        Color c = new Color(0.0f, 1.0f, 0.0f);
        helloText.SetColor(ref c);

        // Align Text center-screen
        helloText.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        helloText.SetVerticalAlignment(VerticalAlignment.CENTER);

        // Add Text instance to the UI root element
        GetSubsystem<UI>().GetRoot().AddChild(helloText);
    }

/*    void SubscribeToEvents()
    {
        // Subscribe HandleUpdate() function for processing update events
        SubscribeToEvent(E_UPDATE, URHO3D_HANDLER(HelloWorld, HandleUpdate));
    }

    void HandleUpdate(StringHash eventType, VariantMap& eventData)
    {
        // Do nothing for now, could be extended to eg. animate the display
    }*/
}
