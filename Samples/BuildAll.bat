set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"

call Clear.bat

csc /target:exe /out:01_HelloWorld.exe /reference:DotNetBindings.dll /platform:x86 01_HelloWorld\*.cs *.cs


csc /target:exe /out:34_DynamicGeometry.exe /reference:DotNetBindings.dll /platform:x86 34_DynamicGeometry\*.cs *.cs

pause
