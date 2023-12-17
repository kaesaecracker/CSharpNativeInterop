using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpWrapper;

public static partial class Hello
{
    [LibraryImport("NativeLibrary", EntryPoint = "_ZN13nativelibrary5HelloEv")]
    [UnmanagedCallConv(CallConvs = new[]{ typeof(CallConvCdecl)})]
    private static partial void NativeHello();

    public static void PrintHelloWorld() => NativeHello();
}
