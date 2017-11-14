using System;
using System.Runtime.InteropServices;

public struct StringHash
{
    public uint Value;
    
    public StringHash(uint value)
    {
        Value = value;
    }
    
    public StringHash(string str)
    {
        Value = Calculate(str);
    }

    [DllImport(Consts.NativeLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint="StringHash_Calculate")]
    public static extern uint Calculate(string str);
}
