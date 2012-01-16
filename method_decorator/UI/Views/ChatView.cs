using System;
using System.Windows.Forms;

namespace UI.Views
{
    public partial class ChatView : Form, IDisplayChatDialog
    {
        public Form TheForm
        {
            get { return this; }
        }

        public ChatView(int a)
        {
            InitializeComponent();
        }

        public void AddMessage(string message, string user)
        {
            uxDialogLabel.Text += string.Format("{0}:\t{1}{2}", user, message, Environment.NewLine);
        }
    }
}