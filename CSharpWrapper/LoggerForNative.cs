using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace NativeLibrary;

public sealed partial class LoggerForNative: IDisposable
{
    private bool _disposed;
    private readonly ILogger _logger;

    // instance is needed to keep reference alive
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly LogMethod _logMethod;

    internal IntPtr NativeHandle { get; }

    public LoggerForNative(ILogger logger)
    {
        _logger = logger;
        _logMethod = Log;
        NativeHandle = ILogger_Constructor(_logMethod);
    }

    private delegate void LogMethod(LogLevel level, string message);

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial IntPtr ILogger_Constructor(LogMethod logMethod);

    [LibraryImport(nameof(NativeLibrary), StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void ILogger_Destructor(IntPtr instance);

    private void Log(LogLevel level, string message)
    {
        _logger.Log(level, "{Handle} {Message}", NativeHandle, message);
    }

    ~LoggerForNative() => Dispose();

    public void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        GC.SuppressFinalize(this);
        ILogger_Destructor(NativeHandle);
    }
}
