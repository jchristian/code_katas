using System.Windows.Forms;

namespace UI.Views
{
    public interface IDisplayChatDialog
    {
        Form TheForm { get; }
        void AddMessage(string message, string user);
    }
}