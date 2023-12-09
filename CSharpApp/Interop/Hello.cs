using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpApp.Interop;

public static partial class Hello
{

    [LibraryImport("NativeLibrary", EntryPoint = "_Z5Hellov")]
    [UnmanagedCallConv(CallConvs = new[]{ typeof(CallConvCdecl)})]
    public static partial void PrintHelloWorld();

}
