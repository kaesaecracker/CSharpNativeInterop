using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace NativeLibrary;

public sealed partial class LoadGenerator : IDisposable
{
    private IntPtr _handle;

    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    // we have to keep references to the delegates so they are not freed
    private readonly LoggerForNative _loggerForNative;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    public LoadGenerator(ILogger<LoadGenerator> logger)
    {
        _loggerForNative = new LoggerForNative(logger);
        _handle = LoadGenerator_Constructor(_loggerForNative.NativeHandle);
    }

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial IntPtr LoadGenerator_Constructor(IntPtr loggerForNative);

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void LoadGenerator_Destructor(IntPtr instance);

    ~LoadGenerator() => Dispose();

    public void Dispose()
    {
        if (_handle == IntPtr.Zero)
            return;
        GC.SuppressFinalize(this);
        LoadGenerator_Destructor(_handle);
        _handle = IntPtr.Zero;
    }
}
