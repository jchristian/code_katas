using System.Diagnostics;

namespace app
{
    public class LoggingCommand : IDoWork
    {
        readonly IDoWork _work;

        public LoggingCommand(IDoWork work)
        {
            _work = work;
        }

        public void Execute()
        {
            _work.Execute();
            Debug.WriteLine("Executed the command " + _work);
        }
    }
}