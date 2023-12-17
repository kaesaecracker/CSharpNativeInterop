using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace CSharpWrapper;

using MessageHandle = long;

public sealed record class NativeMessage(MessageHandle Handle, string Message);

public sealed partial class NativeLibrary : IDisposable
{
    private IntPtr _handle;

    private delegate MessageHandle PassMessage(string message);

    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    // we have to keep references to the delegates so they are not freed
    private readonly LoggerForNative _loggerForNative;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    public NativeLibrary(ILogger<NativeLibrary> logger)
    {
        _loggerForNative = new LoggerForNative(logger);
        _handle = Initialize(_loggerForNative.NativeHandle);
    }

    [LibraryImport("NativeLibrary", EntryPoint = "_ZN13nativelibrary25NativeLibrary_ConstructorEPNS_7ILoggerE",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial IntPtr Initialize(IntPtr loggerForNative);


    [LibraryImport("NativeLibrary", EntryPoint = "_ZN13nativelibrary24NativeLibrary_DestructorEPKNS_13NativeLibraryE",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void NativeDestroy(IntPtr instance);


    [LibraryImport("NativeLibrary", EntryPoint = "_ZN13nativelibrary18NativeLibrary_TestEPKNS_13NativeLibraryE",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void Test(IntPtr instance);

    public void Test() => Test(_handle);

    ~NativeLibrary() => Dispose();

    public void Dispose()
    {
        if (_handle == IntPtr.Zero)
            return;
        GC.SuppressFinalize(this);
        NativeDestroy(_handle);
        _handle = IntPtr.Zero;
    }
}
