
using System;
using System.Linq;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            var blockBoard = new BlockBoard();
            blockBoard.AddBlock(1, 1);

            var consoleWriter = new ConsoleWriter();

            consoleWriter.Write(blockBoard);
            blockBoard.ApplyGravity();
            consoleWriter.Write(blockBoard);
        }
    }

    public class ConsoleWriter
    {
        public void Write(BlockBoard blockBoard)
        {
            foreach(var row in blockBoard.Rows.Reverse())
            {
                Console.WriteLine(row.Cells.Aggregate("", (x, y) => x + CellRepresentation(y)));
            }
        }

        string CellRepresentation(bool hasBlock)
        {
            return hasBlock ? "|X|" : "   ";
        }
    }
}
