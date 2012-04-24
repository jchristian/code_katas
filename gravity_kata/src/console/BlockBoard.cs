using System.Collections.Generic;
using System.Linq;

namespace console
{
    public class BlockBoard
    {
        IList<BlockRow> _rows;
        public IEnumerable<BlockRow> Rows
        {
            get { return _rows; }
        }

        public BlockBoard()
        {
            _rows = new List<BlockRow>();
        }

        public BlockBoard(IEnumerable<IEnumerable<bool>> grid)
        {
            _rows = new List<BlockRow>();

            foreach (var row in grid)
            {
                var blockRow = new BlockRow(row.Count());

                var yPosition = 0;
                foreach (var cell in row)
                {
                    if (cell)
                        blockRow.AddBlock(yPosition);

                    yPosition++;
                }

                _rows.Add(blockRow);
            }
        }

        public void AddBlock(int x_position, int y_position)
        {
            FillWithRowsToPosition(y_position);

            _rows[y_position].AddBlock(x_position);
        }

        void FillWithRowsToPosition(int y_position)
        {
            if (_rows.Count <= y_position)
                _rows = _rows.Concat(Enumerable.Range(0, y_position - _rows.Count + 1).Select(x => new BlockRow())).ToList();
        }

        public void ApplyGravity()
        {
            var newRows = new List<BlockRow>();
            
            var lowerRow = _rows.FirstOrDefault();
            foreach(var higherRow in _rows.Skip(1))
            {
                if (higherRow == null || lowerRow == null)
                    return;

                var newHigherRow = new BlockRow();
                var newLowerRow = new BlockRow();

                var higherRowEnumerator = higherRow.Cells.GetEnumerator();
                var lowerRowEnumerator = lowerRow.Cells.GetEnumerator();

                int x_position = 0;
                while(higherRowEnumerator.MoveNext() || lowerRowEnumerator.MoveNext())
                {
                    if (lowerRowEnumerator.Current && higherRowEnumerator.Current)
                        newHigherRow.AddBlock(x_position); 
                    
                    if (lowerRowEnumerator.Current || higherRowEnumerator.Current)
                        newLowerRow.AddBlock(x_position);

                    x_position++;
                }

                newRows.Add(newLowerRow);
                lowerRow = newHigherRow;
            }

            _rows = newRows;
        }

        public void ShiftRowRight(int theRowIndex)
        {
            _rows[theRowIndex].ShiftRight();
        }
    }
}