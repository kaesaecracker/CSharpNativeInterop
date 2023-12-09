using System;

namespace CSharpApp.Interop;

public class StatusCodeException(long StatusCode): Exception
{
    public static void ThrowIfNonZero(long statusCode)
    {
        if (statusCode != 0L)
            throw new StatusCodeException(statusCode);
    }
}
