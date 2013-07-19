using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        [PublicAPI]
        public void ResetCache()
        {
            _rowList = new RowList(this);
        }

        [PublicAPI]
        public IReadOnlyList<TRow> Rows
        {
            get
            {
                return _rowList;
            }
        }

        [PublicAPI]
        public TRow FindRow(Predicate<TRow> rowPredicate)
        {
            foreach (var row in Rows)
            {
                if (Animate) row.Hover();
                if (rowPredicate(row))
                {
                    if (Animate) row.DrawHighlight();
                    return row;
                }
            }
            return null;
        }

        [PublicAPI]
        public void AssertRowExists(Predicate<TRow> rowPredicate, string message)
        {
            if (FindRow(rowPredicate) != null) return;
            throw new AssertFailedException("Row should exist but does not. " + message);
        }

        [PublicAPI]
        public void AssertRowDoesNotExist(Predicate<TRow> rowPredicate, string message)
        {
            if (FindRow(rowPredicate) == null) return;
            throw new AssertFailedException("Row should not exist but does. " + message);
        }

    }
}