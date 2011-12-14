
using System;
using System.Diagnostics;

namespace app
{
    public class TimingCommand : IDoWork
    {
        IDoWork _work;

        public TimingCommand(IDoWork work)
        {
            _work = work;
        }

        public void Execute()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _work.Execute();
            stopwatch.Stop();
            Console.WriteLine(string.Format("Time to execute: {0}ms", stopwatch.ElapsedMilliseconds));
        }
    }
}