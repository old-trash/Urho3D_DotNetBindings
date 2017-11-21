set "PATH=c:\windows\microsoft.net\framework\v4.0.30319\"
del HeaderParser.exe
csc /target:exe /out:HeaderParser.exe *.cs
pause
