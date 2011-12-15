using System.Threading;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            var command_that_logs = CommandFactory.create_with_loggin(() => Thread.CurrentThread.Join(1000));
            var command_that_times = CommandFactory.create_with_timing(() => Thread.CurrentThread.Join(5000));
            var command_that_logs_and_times = CommandFactory.create_with_timing_and_logging(() => Thread.CurrentThread.Join(5000));

            command_that_logs.Execute();
            command_that_times.Execute();
            command_that_logs_and_times.Execute();
        }
    }
}
