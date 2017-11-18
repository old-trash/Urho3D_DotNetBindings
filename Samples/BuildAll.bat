set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"

call Clear.bat

csc /target:exe /debug:full /out:01_HelloWorld.exe /reference:DotNetBindings.dll /platform:x86 01_HelloWorld\*.cs *.cs

pause
