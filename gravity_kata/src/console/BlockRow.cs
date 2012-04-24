using System.Collections.Generic;
using System.Linq;

namespace console
{
    public class BlockRow
    {
        IList<bool> _cells;
        public IEnumerable<bool> Cells
        {
            get { return _cells; }
        }

        public BlockRow()
        {
            _cells = new List<bool>();
        }

        public BlockRow(int count) : this(Enumerable.Range(1, count).Select(x => false))
        { }

        public BlockRow(IEnumerable<bool> count)
        {
            _cells = count.ToList();
        }

        public void AddBlock(int position)
        {
            FillWithCellsToPosition(position);

            _cells[position] = true;
        }

        void FillWithCellsToPosition(int position)
        {
            if (_cells.Count <= position)
                _cells = _cells.Concat(Enumerable.Range(0, position - _cells.Count + 1).Select(x => false)).ToList();
        }

        public void ShiftRight()
        {
            _cells.Insert(0, false);
        }
    }
}