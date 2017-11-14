# Urho3D_DotNetBindings

### 1. Building Native Library
Build https://github.com/1vanK/Urho3D as x32 DLL.

### 2. Building Bindings
1) If you compiled the DLL as Release, in file "Bindings/Consts.cs" change "Urho3D_d" to "Urho3D".
2) Run "Bindings/Build.bat".

### 3. Test
1) Copy "Urho3D(_d).dll" to TestApp folder.
2) Copy "DotNetBindings.dll" to TestApp folder.
3) Copy "Data" and "CoreData" to TestApp folder.
4) Run "TestApp/Build.bat".
5) Run "TestApp/Test.exe" or "TestApp/Test.bat". You should see a black window. Alt+F4 to exit.

