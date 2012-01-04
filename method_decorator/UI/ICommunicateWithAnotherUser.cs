using System;

namespace UI
{
    public interface ICommunicateWithAnotherUser
    {
        event Action<string, string> MessageReceived;
    }
}