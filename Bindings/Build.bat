set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"
del DotNetBindings.dll
csc /target:library /out:DotNetBindings.dll /platform:x86 Container\*.cs Core\*.cs Engine\*.cs Input\*.cs IO\*.cs Math\*.cs *.cs
pause
