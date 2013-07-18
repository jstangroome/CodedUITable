using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodedUITable
{
    public class Table<TRow> : Table where TRow : TableRow, new()
    {
        class RowList : IReadOnlyList<TRow>
        {
            private readonly Table<TRow> _parentTable;
            private readonly LazyDictionary<int, TRow> _rowCache;

            public RowList(Table<TRow> parentTable)
            {
                _parentTable = parentTable;
                _rowCache = new LazyDictionary<int, TRow>(CreateRow);
            }

            private TRow CreateRow(int bodyRowIndex)
            {
                var cells = _parentTable.GetHtmlCellsForBodyRow(bodyRowIndex);
                var row = new TRow();
                row.Initialize(_parentTable.HtmlTable, cells);
                return row;
            }

            public IEnumerator<TRow> GetEnumerator()
            {
                var bodyRowIndex = 0;
                while (bodyRowIndex < Count)
                {
                    yield return this[bodyRowIndex];
                    bodyRowIndex++;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count
            {
                get { return _parentTable.RowCount; }
            }

            public TRow this[int index]
            {
                get { return _rowCache.Get(index); }
            }
        }

        private RowList _rowList;
        
        public Table(HtmlTable htmlTable) : this(htmlTable, 1, 0)
        {
        }

        public Table(HtmlTable htmlTable, int headerRowCount, int footerRowCount)
            : base(htmlTable, headerRowCount, footerRowCount)
        {
            ResetCache();
        }

        public void ResetCache()
        {
            _rowList = new RowList(this);
        }

        public IReadOnlyList<TRow> Rows
        {
            get
            {
                return _rowList;
            }
        }

        public void AssertRowExists(Predicate<TRow> rowPredicate, string message)
        {
            foreach (var row in Rows)
            {
                if (Animate) row.DrawHighlight();
                if (rowPredicate(row)) return;
            }
            throw new AssertFailedException("Row should exist but doesn't. " + message);
        }


    }
}