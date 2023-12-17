using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NativeLibrary;

public static partial class Hello
{
    [LibraryImport(nameof(NativeLibrary))]
    [UnmanagedCallConv(CallConvs = new[]{ typeof(CallConvCdecl)})]
    private static partial void print_hello_world();

    public static void PrintHelloWorld() => print_hello_world();
}
