set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"
del Test.exe
csc /target:exe /out:Test.exe /reference:DotNetBindings.dll /platform:x86 *.cs
pause
