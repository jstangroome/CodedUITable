using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CodedUITable
{
    public abstract class TableRow
    {
        private HtmlTable _htmlTable;
        public int RowIndex { get; private set; }

        private LazyDictionary<int, HtmlCell> _htmlCellCache;
        private Lazy<ColumnMetadata[]> _columnMetadataCache;
        private Lazy<ColumnMetadata> _defaultColumnCache; 

        public void Initialize(HtmlTable htmlTable, HtmlCell[] htmlCells)
        {
            if (_htmlTable != null)
            {
                throw new InvalidOperationException("Row is already initialized.");
            }

            if (htmlCells.Select(c => c.RowIndex).Distinct().Count() != 1)
            {
                throw new ArgumentException("All cells must have the same RowIndex.", "htmlCells");
            }

            _htmlTable = htmlTable;
            RowIndex = htmlCells[0].RowIndex;

            ResetCache();
            foreach (var htmlCell in htmlCells)
            {
                _htmlCellCache.Set(htmlCell.ColumnIndex, htmlCell);
            }
        }

        public void ResetCache()
        {
            _htmlCellCache = new LazyDictionary<int, HtmlCell>(columnIndex => (HtmlCell)_htmlTable.GetCell(RowIndex, columnIndex));
            _columnMetadataCache = new Lazy<ColumnMetadata[]>(GetColumnMetadata, LazyThreadSafetyMode.PublicationOnly);
            _defaultColumnCache = new Lazy<ColumnMetadata>(() => GetDefaultColumn(_columnMetadataCache.Value), LazyThreadSafetyMode.PublicationOnly);
        }

        public void DrawHighlight()
        {
            _htmlTable.GetRow(RowIndex).DrawHighlight();
        }

        protected virtual ColumnMetadata[] GetColumnMetadata()
        {
            var rowType = GetType();
            var columns = new List<ColumnMetadata>();
            foreach (var propertyInfo in rowType.GetProperties())
            {
                var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null)
                {
                    columns.Add(new ColumnMetadata(columnAttribute.ColumnIndex, propertyInfo.Name));
                }
            }
            return columns.ToArray();
        }

        protected virtual ColumnMetadata GetDefaultColumn(ColumnMetadata[] columns)
        {
            return columns[0];
        }

        protected string GetColumnValue([CallerMemberName] string propertyName = null)
        {
            return GetHtmlCell(propertyName).InnerText;
        }

        private HtmlCell GetHtmlCell(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            var column = _columnMetadataCache.Value.FirstOrDefault(c => c.Name == propertyName);

            if (column == null)
            {
                throw new InvalidOperationException(string.Format("Column metadata not defined for property '{0}'.", propertyName));
            }

            return _htmlCellCache.Get(column.Index);
        }
  
        private HtmlCell GetDefaultCell()
        {
            return _htmlCellCache.Get(_defaultColumnCache.Value.Index);
        }

        public void Click()
        {
            Mouse.Click(GetDefaultCell());
        }

        public void Hover()
        {
            Mouse.Hover(GetDefaultCell());
        }

    }
}