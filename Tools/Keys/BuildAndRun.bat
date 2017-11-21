set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"
del Keys.exe
csc /target:exe /out:Keys.exe *.cs
Keys.exe
del Keys.exe
pause
