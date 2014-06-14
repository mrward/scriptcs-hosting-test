
var scriptPack = Require<MyScriptPackContext>();

using System;

Console.WriteLine("Hello from script");
Console.WriteLine(scriptPack.GetString());
scriptPack.UpdateString("foo");
Console.WriteLine(scriptPack.GetString());