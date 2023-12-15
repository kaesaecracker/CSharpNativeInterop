using System;
using System.Threading;
using CSharpWrapper;

long lastMessage = 0;

var waitMs = 5000;
if (args.Length == 1 && !int.TryParse(args[0], out waitMs))
    Console.Error.WriteLine($"invalid number {args[0]}");

using (var lib = new NativeLibrary(OnMessage))
{
    lib.Test();

    Console.WriteLine("main thread sleeps");
    Thread.Sleep(waitMs);
    Console.WriteLine("main thread wakes up");

    Hello.PrintHelloWorld();
}

Console.WriteLine($"processed {lastMessage} messages");
return;


void OnMessage(NativeMessage message)
{
    lastMessage = message.Handle;
    Console.WriteLine($"received {message}");
}
