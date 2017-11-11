#include "../../Engine/Application.h"

using namespace Urho3D;

extern "C"
{

URHO3D_API Application* Application_Application(Context* context)
{
    return new Application(context);
}

URHO3D_API int Application_Run(Application* nativeObject)
{
    return nativeObject->Run();
}

}
