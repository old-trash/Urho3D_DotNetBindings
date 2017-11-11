#include "../../Engine/Application.h"

using namespace Urho3D;

extern "C"
{

URHO3D_API Application* Application_Application(Context* nativeContext)
{
    return new Application(nativeContext);
}

URHO3D_API int Application_Run(Application* nativeInstance)
{
    return nativeInstance->Run();
}

}
