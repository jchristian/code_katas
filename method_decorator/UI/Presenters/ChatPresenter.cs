using UI.Views;

namespace UI.Presenters
{
    public class ChatPresenter
    {
        public IDisplayChatDialog TheView { get; set; }

        public ChatPresenter(IDisplayChatDialog theView, ICommunicateWithAnotherUser communication)
        {
            TheView = theView;

            communication.MessageReceived += Receive;
        }

        public void Send(string message)
        {
            TheView.AddMessage(message, "You");
        }

        public void Receive(string message, string user)
        {
            TheView.AddMessage(message, user);
        }
    }
}