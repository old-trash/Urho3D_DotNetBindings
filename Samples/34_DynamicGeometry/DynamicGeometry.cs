using System;

class DynamicGeometry : Sample
{
    static int Main()
    {
        ProcessUtils.ParseArguments(Environment.CommandLine);
        DynamicGeometry app = new DynamicGeometry(new Context());
        return app.Run();
    }
    
    public DynamicGeometry(Context context) : base(context)
    {
    }
    
    public override void Start()
    {
        // Execute base class startup
        base.Start();

        // Create the scene content
        CreateScene();

        // Create the UI content
        //CreateInstructions();

        // Setup the viewport for displaying the scene
        SetupViewport();

        // Hook up to the frame update events
        //SubscribeToEvents();

        // Set the mouse mode to use in the sample
        //Sample::InitMouseMode(MM_RELATIVE);
    }
    
    private Scene scene_;
    private Node cameraNode_;
    private Camera camera_;
    
    private void CreateScene()
    {
        ResourceCache cache = GetSubsystem<ResourceCache>();
    
        scene_ = new Scene(context_);
    
        // Create the Octree component to the scene so that drawable objects can be rendered. Use default volume
        // (-1000, -1000, -1000) to (1000, 1000, 1000)
        scene_.CreateComponent<Octree>();
    
        // Create a Zone for ambient light & fog control
        Node zoneNode = scene_.CreateChild("Zone");
        Zone zone = zoneNode.CreateComponent<Zone>();
        //zone->SetBoundingBox(BoundingBox(-1000.0f, 1000.0f));
        zone.SetFogColor(new Color(0.2f, 1.0f, 0.2f));
        //zone->SetFogStart(200.0f);
        //zone->SetFogEnd(300.0f);
    
        // Create a directional light
        /*Node* lightNode = scene_->CreateChild("DirectionalLight");
        lightNode->SetDirection(Vector3(-0.6f, -1.0f, -0.8f)); // The direction vector does not need to be normalized
        Light* light = lightNode->CreateComponent<Light>();
        light->SetLightType(LIGHT_DIRECTIONAL);
        light->SetColor(Color(0.4f, 1.0f, 0.4f));
        light->SetSpecularIntensity(1.5f);
    
        // Get the original model and its unmodified vertices, which are used as source data for the animation
        Model* originalModel = cache->GetResource<Model>("Models/Box.mdl");
        if (!originalModel)
        {
            URHO3D_LOGERROR("Model not found, cannot initialize example scene");
            return;
        }
        // Get the vertex buffer from the first geometry's first LOD level
        VertexBuffer* buffer = originalModel->GetGeometry(0, 0)->GetVertexBuffer(0);
        const unsigned char* vertexData = (const unsigned char*)buffer->Lock(0, buffer->GetVertexCount());
        if (vertexData)
        {
            unsigned numVertices = buffer->GetVertexCount();
            unsigned vertexSize = buffer->GetVertexSize();
            // Copy the original vertex positions
            for (unsigned i = 0; i < numVertices; ++i)
            {
                const Vector3& src = *reinterpret_cast<const Vector3*>(vertexData + i * vertexSize);
                originalVertices_.Push(src);
            }
            buffer->Unlock();
    
            // Detect duplicate vertices to allow seamless animation
            vertexDuplicates_.Resize(originalVertices_.Size());
            for (unsigned i = 0; i < originalVertices_.Size(); ++i)
            {
                vertexDuplicates_[i] = i; // Assume not a duplicate
                for (unsigned j = 0; j < i; ++j)
                {
                    if (originalVertices_[i].Equals(originalVertices_[j]))
                    {
                        vertexDuplicates_[i] = j;
                        break;
                    }
                }
            }
        }
        else
        {
            URHO3D_LOGERROR("Failed to lock the model vertex buffer to get original vertices");
            return;
        }
    
        // Create StaticModels in the scene. Clone the model for each so that we can modify the vertex data individually
        for (int y = -1; y <= 1; ++y)
        {
            for (int x = -1; x <= 1; ++x)
            {
                Node* node = scene_->CreateChild("Object");
                node->SetPosition(Vector3(x * 2.0f, 0.0f, y * 2.0f));
                StaticModel* object = node->CreateComponent<StaticModel>();
                SharedPtr<Model> cloneModel = originalModel->Clone();
                object->SetModel(cloneModel);
                // Store the cloned vertex buffer that we will modify when animating
                animatingBuffers_.Push(SharedPtr<VertexBuffer>(cloneModel->GetGeometry(0, 0)->GetVertexBuffer(0)));
            }
        }
    
        // Finally create one model (pyramid shape) and a StaticModel to display it from scratch
        // Note: there are duplicated vertices to enable face normals. We will calculate normals programmatically
        {
            const unsigned numVertices = 18;
    
            float vertexData[] = {
                // Position             Normal
                0.0f, 0.5f, 0.0f,       0.0f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f,      0.0f, 0.0f, 0.0f,
                0.5f, -0.5f, -0.5f,     0.0f, 0.0f, 0.0f,
    
                0.0f, 0.5f, 0.0f,       0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,     0.0f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f,      0.0f, 0.0f, 0.0f,
    
                0.0f, 0.5f, 0.0f,       0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,    0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,     0.0f, 0.0f, 0.0f,
    
                0.0f, 0.5f, 0.0f,       0.0f, 0.0f, 0.0f,
                0.5f, -0.5f, -0.5f,     0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,    0.0f, 0.0f, 0.0f,
    
                0.5f, -0.5f, -0.5f,     0.0f, 0.0f, 0.0f,
                0.5f, -0.5f, 0.5f,      0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,     0.0f, 0.0f, 0.0f,
    
                0.5f, -0.5f, -0.5f,     0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, 0.5f,     0.0f, 0.0f, 0.0f,
                -0.5f, -0.5f, -0.5f,    0.0f, 0.0f, 0.0f
            };
    
            const unsigned short indexData[] = {
                0, 1, 2,
                3, 4, 5,
                6, 7, 8,
                9, 10, 11,
                12, 13, 14,
                15, 16, 17
            };
    
            // Calculate face normals now
            for (unsigned i = 0; i < numVertices; i += 3)
            {
                Vector3& v1 = *(reinterpret_cast<Vector3*>(&vertexData[6 * i]));
                Vector3& v2 = *(reinterpret_cast<Vector3*>(&vertexData[6 * (i + 1)]));
                Vector3& v3 = *(reinterpret_cast<Vector3*>(&vertexData[6 * (i + 2)]));
                Vector3& n1 = *(reinterpret_cast<Vector3*>(&vertexData[6 * i + 3]));
                Vector3& n2 = *(reinterpret_cast<Vector3*>(&vertexData[6 * (i + 1) + 3]));
                Vector3& n3 = *(reinterpret_cast<Vector3*>(&vertexData[6 * (i + 2) + 3]));
    
                Vector3 edge1 = v1 - v2;
                Vector3 edge2 = v1 - v3;
                n1 = n2 = n3 = edge1.CrossProduct(edge2).Normalized();
            }
    
            SharedPtr<Model> fromScratchModel(new Model(context_));
            SharedPtr<VertexBuffer> vb(new VertexBuffer(context_));
            SharedPtr<IndexBuffer> ib(new IndexBuffer(context_));
            SharedPtr<Geometry> geom(new Geometry(context_));
    
            // Shadowed buffer needed for raycasts to work, and so that data can be automatically restored on device loss
            vb->SetShadowed(true);
            // We could use the "legacy" element bitmask to define elements for more compact code, but let's demonstrate
            // defining the vertex elements explicitly to allow any element types and order
            PODVector<VertexElement> elements;
            elements.Push(VertexElement(TYPE_VECTOR3, SEM_POSITION));
            elements.Push(VertexElement(TYPE_VECTOR3, SEM_NORMAL));
            vb->SetSize(numVertices, elements);
            vb->SetData(vertexData);
    
            ib->SetShadowed(true);
            ib->SetSize(numVertices, false);
            ib->SetData(indexData);
    
            geom->SetVertexBuffer(0, vb);
            geom->SetIndexBuffer(ib);
            geom->SetDrawRange(TRIANGLE_LIST, 0, numVertices);
    
            fromScratchModel->SetNumGeometries(1);
            fromScratchModel->SetGeometry(0, 0, geom);
            fromScratchModel->SetBoundingBox(BoundingBox(Vector3(-0.5f, -0.5f, -0.5f), Vector3(0.5f, 0.5f, 0.5f)));
    
            // Though not necessary to render, the vertex & index buffers must be listed in the model so that it can be saved properly
            Vector<SharedPtr<VertexBuffer> > vertexBuffers;
            Vector<SharedPtr<IndexBuffer> > indexBuffers;
            vertexBuffers.Push(vb);
            indexBuffers.Push(ib);
            // Morph ranges could also be not defined. Here we simply define a zero range (no morphing) for the vertex buffer
            PODVector<unsigned> morphRangeStarts;
            PODVector<unsigned> morphRangeCounts;
            morphRangeStarts.Push(0);
            morphRangeCounts.Push(0);
            fromScratchModel->SetVertexBuffers(vertexBuffers, morphRangeStarts, morphRangeCounts);
            fromScratchModel->SetIndexBuffers(indexBuffers);
    
            Node* node = scene_->CreateChild("FromScratchObject");
            node->SetPosition(Vector3(0.0f, 3.0f, 0.0f));
            StaticModel* object = node->CreateComponent<StaticModel>();
            object->SetModel(fromScratchModel);
        }*/
    
        // Create the camera
        cameraNode_ = scene_.CreateChild();
        cameraNode_.SetPosition(new Vector3(0.0f, 0.0f, 0.0f));
        camera_ = cameraNode_.CreateComponent<Camera>();
        //camera->SetFarClip(300.0f);
    }
    
    void SetupViewport()
    {
        Renderer renderer = GetSubsystem<Renderer>();
    
        // Set up a viewport to the Renderer subsystem so that the 3D scene can be seen
        Viewport viewport = new Viewport(context_, scene_, camera_);
        renderer.SetViewport(0, viewport);
    }

    
}