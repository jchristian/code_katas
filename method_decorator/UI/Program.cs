using System;
using System.Windows.Forms;

namespace UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var presenter = new ChatPresenter(new ChatView().Decorate(x => x.AddMessage(null, null)).With(InvokeRequired.Invoke), new CommunicationProxy());
            //Application.Run(presenter.TheView.TheForm);
        }
    }
}