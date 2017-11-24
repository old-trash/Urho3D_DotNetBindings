using System;

class StaticScene : Sample
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
        StaticScene app = new StaticScene(new Context());
        return app.Run();
    }

    public StaticScene(Context context) : base(context)
    {
    }
    
    public override void Start()
    {
        // Execute base class startup
        base.Start();
    
        // Create the scene content
        CreateScene();
    
        // Create the UI content
        CreateInstructions();
    
        // Setup the viewport for displaying the scene
        SetupViewport();
    
        // Hook up to the frame update events
        SubscribeToEvents();
    
        // Set the mouse mode to use in the sample
        //Sample::InitMouseMode(MM_RELATIVE);
    }
    
    void CreateScene()
    {
        ResourceCache cache = GetSubsystem<ResourceCache>();
    
        scene_ = new Scene(context_);
    
        // Create the Octree component to the scene. This is required before adding any drawable components, or else nothing will
        // show up. The default octree volume will be from (-1000, -1000, -1000) to (1000, 1000, 1000) in world coordinates; it
        // is also legal to place objects outside the volume but their visibility can then not be checked in a hierarchically
        // optimizing manner
        scene_.CreateComponent<Octree>();
    
        // Create a child scene node (at world origin) and a StaticModel component into it. Set the StaticModel to show a simple
        // plane mesh with a "stone" material. Note that naming the scene nodes is optional. Scale the scene node larger
        // (100 x 100 world units)
        Node planeNode = scene_.CreateChild("Plane");
        planeNode.SetScale(new Vector3(100.0f, 1.0f, 100.0f));
        StaticModel planeObject = planeNode.CreateComponent<StaticModel>();
        planeObject.SetModel(cache.GetResource<Model>("Models/Plane.mdl"));
        planeObject.SetMaterial(cache.GetResource<Material>("Materials/StoneTiled.xml"));
    
        // Create a directional light to the world so that we can see something. The light scene node's orientation controls the
        // light direction; we will use the SetDirection() function which calculates the orientation from a forward direction vector.
        // The light will use default settings (white light, no shadows)
        Node lightNode = scene_.CreateChild("DirectionalLight");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f)); // The direction vector does not need to be normalized
        Light light = lightNode.CreateComponent<Light>();
        light.SetLightType(LightType.LIGHT_DIRECTIONAL);
    
        // Create more StaticModel objects to the scene, randomly positioned, rotated and scaled. For rotation, we construct a
        // quaternion from Euler angles where the Y angle (rotation about the Y axis) is randomized. The mushroom model contains
        // LOD levels, so the StaticModel component will automatically select the LOD level according to the view distance (you'll
        // see the model get simpler as it moves further away). Finally, rendering a large number of the same object with the
        // same material allows instancing to be used, if the GPU supports it. This reduces the amount of CPU work in rendering the
        // scene.
        const uint NUM_OBJECTS = 200;
        for (uint i = 0; i < NUM_OBJECTS; ++i)
        {
            Node mushroomNode = scene_.CreateChild("Mushroom");
            mushroomNode.SetPosition(new Vector3(Math.Random(90.0f) - 45.0f, 0.0f, Math.Random(90.0f) - 45.0f));
            mushroomNode.SetRotation(Quaternion.FromEulerAngles(0.0f, Math.Random(360.0f), 0.0f));
            mushroomNode.SetScale(0.5f + Math.Random(2.0f));
            StaticModel mushroomObject = mushroomNode.CreateComponent<StaticModel>();
            mushroomObject.SetModel(cache.GetResource<Model>("Models/Mushroom.mdl"));
            mushroomObject.SetMaterial(cache.GetResource<Material>("Materials/Mushroom.xml"));
        }
    
        // Create a scene node for the camera, which we will move around
        // The camera will use default settings (1000 far clip distance, 45 degrees FOV, set aspect ratio automatically)
        cameraNode_ = scene_.CreateChild("Camera");
        cameraNode_.CreateComponent<Camera>();
    
        // Set an initial position for the camera scene node above the plane
        cameraNode_.SetPosition(new Vector3(0.0f, 5.0f, 0.0f));
    }
    
    void CreateInstructions()
    {
        ResourceCache cache = GetSubsystem<ResourceCache>();
        UI ui = GetSubsystem<UI>();
    
        // Construct new Text object, set string to display and font to use
        Text instructionText = ui.GetRoot().CreateChild<Text>();
        instructionText.SetText("Use WASD keys and mouse/touch to move");
        instructionText.SetFont(cache.GetResource<Font>("Fonts/Anonymous Pro.ttf"), 15);
    
        // Position the text relative to the screen center
        instructionText.SetHorizontalAlignment(HorizontalAlignment.HA_CENTER);
        instructionText.SetVerticalAlignment(VerticalAlignment.VA_CENTER);
        instructionText.SetPosition(0, ui.GetRoot().GetHeight() / 4);
    }

    void SetupViewport()
    {
        Renderer renderer = GetSubsystem<Renderer>();
    
        // Set up a viewport to the Renderer subsystem so that the 3D scene can be seen. We need to define the scene and the camera
        // at minimum. Additionally we could configure the viewport screen size and the rendering path (eg. forward / deferred) to
        // use, but now we just use full screen and default render path configured in the engine command line options
        Viewport viewport = new Viewport(context_, scene_, cameraNode_.GetComponent<Camera>());
        renderer.SetViewport(0, viewport);
    }
    
    void SubscribeToEvents()
    {
        // Subscribe HandleUpdate() function for processing update events
        SubscribeToEvent("Update", HandleUpdate);
    }
    
    public void HandleUpdate(StringHash eventType, IntPtr eventData)
    {
        VariantMap data = new VariantMap(eventData);
    
        // Take the frame time step, which is stored as a float
        float timeStep = data["TimeStep"].GetFloat();
    
        // Move the camera, scale movement with time step
        MoveCamera(timeStep);
    }
    
    void MoveCamera(float timeStep)
    {
        // Do not move if the UI has a focused element (the console)
        //if (GetSubsystem<UI>().GetFocusElement())
         //   return;
    
        Input input = GetSubsystem<Input>();
    
        // Movement speed as world units per second
        const float MOVE_SPEED = 20.0f;
        // Mouse sensitivity as degrees per pixel
        const float MOUSE_SENSITIVITY = 0.1f;
    
        // Use this frame's mouse motion to adjust camera node yaw and pitch. Clamp the pitch between -90 and 90 degrees
        IntVector2 mouseMove = input.GetMouseMove();
        yaw_ += MOUSE_SENSITIVITY * mouseMove.x_;
        pitch_ += MOUSE_SENSITIVITY * mouseMove.y_;
        pitch_ = Math.Clamp(pitch_, -90.0f, 90.0f);
    
        // Construct new orientation for the camera scene node from yaw and pitch. Roll is fixed to zero
        cameraNode_.SetRotation(Quaternion.FromEulerAngles(pitch_, yaw_, 0.0f));
    
        // Read WASD keys and move the camera scene node to the corresponding direction if they are pressed
        // Use the Translate() function (default local space) to move relative to the node's orientation.
        if (input.GetKeyDown(Keys.KEY_W))
            cameraNode_.Translate(Vector3.FORWARD * MOVE_SPEED * timeStep);
        if (input.GetKeyDown(Keys.KEY_S))
            cameraNode_.Translate(Vector3.BACK * MOVE_SPEED * timeStep);
        if (input.GetKeyDown(Keys.KEY_A))
            cameraNode_.Translate(Vector3.LEFT * MOVE_SPEED * timeStep);
        if (input.GetKeyDown(Keys.KEY_D))
            cameraNode_.Translate(Vector3.RIGHT * MOVE_SPEED * timeStep);
    }
}
