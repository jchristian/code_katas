using System;

namespace app
{
    public class CommandFactory
    {
        public static IDoWork create_with_loggin(Action work)
        {
            return new LoggingCommand(new GenericCommand(work));
        }

        public static IDoWork create_with_timing(Action work)
        {
            return new TimingCommand(new GenericCommand(work));
        }

        public static IDoWork create_with_timing_and_logging(Action work)
        {
            return new LoggingCommand(new TimingCommand(new GenericCommand(work)));
        }
    }
}