using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpApp.Interop;

internal sealed record class NativeMessage(long MessageId, string Message);

internal static partial class NativeLibrary
{
    private static long _messageId;

    public static event EventHandler<NativeMessage>? MessageReceived;

    static NativeLibrary()
    {
        var initStatus = Initialize(OnMessage);
        if (initStatus != 0L)
            throw new StatusCodeException(initStatus);
    }

    private static long OnMessage(string message)
    {
        var msg = new NativeMessage(_messageId++, message);
        Console.WriteLine($"received {msg}");
        MessageReceived?.Invoke(null, msg);
        return msg.MessageId;
    }

    private delegate long PassMessage(string message);

    [LibraryImport("NativeLibrary", EntryPoint = "_Z24NativeLibrary_InitializePFlPKDuE", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[]{ typeof(CallConvCdecl)})]
    private static partial long Initialize(PassMessage passMessage);


    [LibraryImport("NativeLibrary", EntryPoint = "_Z18NativeLibrary_Testv", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = new[]{ typeof(CallConvCdecl)})]
    public static partial void Test();
}
