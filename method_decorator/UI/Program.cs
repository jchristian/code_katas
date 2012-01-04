using System;
using System.Windows.Forms;
using UI.Presenters;
using UI.Views;

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

            var presenter = new ChatPresenter(new ChatView(), new CommunicationProxy());
            Application.Run(presenter.TheView.TheForm);
        }
    }
}