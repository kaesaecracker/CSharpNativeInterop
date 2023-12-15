using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace CSharpWrapper;

using MessageHandle = long;

public sealed record class NativeMessage(MessageHandle Handle, string Message);

public partial class NativeLibrary : IDisposable
{
    private IntPtr _handle;
    private MessageHandle _lastMessageHandle;

    private delegate MessageHandle PassMessage(string message);

    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    // we have to keep references to the delegates so they are not freed
    private readonly PassMessage _onMessageDelegate;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    public NativeLibrary(Action<NativeMessage> onMessage)
    {
        _onMessageDelegate = OnMessageDelegate;
        _handle = Initialize(_onMessageDelegate);
        return;

        MessageHandle OnMessageDelegate(string messageText)
        {
            var messageId = Interlocked.Increment(ref _lastMessageHandle);
            var message = new NativeMessage(messageId, messageText);
            onMessage(message);
            return message.Handle;
        }
    }

    [LibraryImport("NativeLibrary", EntryPoint = "_Z24NativeLibrary_InitializePFlPKDuE",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial IntPtr Initialize(PassMessage passMessage);


    [LibraryImport("NativeLibrary", EntryPoint = "_Z26NativeLibrary_DeinitializePK13NativeLibrary",
        StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static partial void Deinitialize(IntPtr instance);


    [LibraryImport("NativeLibrary", EntryPoint = "_Z18NativeLibrary_TestPK13NativeLibrary",
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
        Deinitialize(_handle);
        _handle = IntPtr.Zero;
    }
}
