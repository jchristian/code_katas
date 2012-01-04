using System;
using System.Linq.Expressions;
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

            var presenter = new ChatPresenter(new ChatView().Decorate(x => x.AddMessage(null, null)).With(InvokeRequired.Invoke), new CommunicationProxy());
            Application.Run(presenter.TheView.TheForm);
        }
    }

    public static class ObjectExtensions
    {
        public static IBuildADecorator<T> Decorate<T>(this T instance, Expression<Action<T>> methodToDecorate)
        {
            return new DecoratorBuilder<T>(methodToDecorate);
        }
    }

    public class DecoratorBuilder<T> : IBuildADecorator<T>
    {
        Expression<Action<T>> _methodToDecorate;

        public DecoratorBuilder(Expression<Action<T>> methodToDecorate)
        {
            _methodToDecorate = methodToDecorate;
        }

        public T With(Action<T> decoration)
        {
            _classBuilder.Build<T>()
                .Substituting(_methodBuilder.Combine(decoration, _methodToDecorate.Compile()))
                .For(_methodToDecorate);
        }
    }

    public interface IBuildADecorator<T>
    {
        T With(Action<T> decoration);
    }
}