using System;

namespace app
{
    public class GenericCommand : IDoWork
    {
        Action _work;

        public GenericCommand(Action work)
        {
            _work = work;
        }

        public void Execute()
        {
            _work();
        }
    }
}