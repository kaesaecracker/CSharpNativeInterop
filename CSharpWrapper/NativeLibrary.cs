using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace NativeLibrary;

public sealed partial class NativeLibrary : IDisposable
{
    private IntPtr _handle;

    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    // we have to keep references to the delegates so they are not freed
    private readonly LoggerForNative _loggerForNative;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    public NativeLibrary(ILogger<NativeLibrary> logger)
    {
        _loggerForNative = new LoggerForNative(logger);
        _handle = NativeLibrary_Constructor(_loggerForNative.NativeHandle);
    }

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial IntPtr NativeLibrary_Constructor(IntPtr loggerForNative);

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void NativeLibrary_Destructor(IntPtr instance);

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void NativeLibrary_Test(IntPtr instance);
    public void Test() => NativeLibrary_Test(_handle);

    ~NativeLibrary() => Dispose();

    public void Dispose()
    {
        if (_handle == IntPtr.Zero)
            return;
        GC.SuppressFinalize(this);
        NativeLibrary_Destructor(_handle);
        _handle = IntPtr.Zero;
    }
}
