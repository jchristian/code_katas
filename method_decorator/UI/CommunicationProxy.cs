using System;
using System.Threading;

namespace UI
{
    public class CommunicationProxy : ICommunicateWithAnotherUser
    {
        public event Action<string, string> MessageReceived;

        public CommunicationProxy()
        {
            ThreadPool.QueueUserWorkItem(callback =>
            {
                var index = 0;
                while(true)
                {
                    Thread.CurrentThread.Join(2000);
                    MessageReceived(string.Format("This is message {0}", index++), "Jonathan");
                    Thread.CurrentThread.Join(5000);
                }
            });
        }
    }
}