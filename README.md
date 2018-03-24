# Urho3D_DotNetBindings

### 1. Building Native Library
Build https://github.com/urho3d/Urho3D/pull/2171 as x32 DLL.

### 2. Building Bindings
1) If you compiled the DLL as Release, in file "Bindings/Consts.cs" change "Urho3D_d" to "Urho3D".
2) Run "Bindings/Build.bat".

### 3. Test
1) Copy "Urho3D(_d).dll" to Samples folder.
2) Copy "DotNetBindings.dll" to Samples folder.
3) Copy "Data" and "CoreData" to Samples folder.
4) Run "Samples/BuildAll.bat".
