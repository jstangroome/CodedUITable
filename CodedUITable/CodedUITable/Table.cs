using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CodedUITable
{
    public abstract class Table
    {
        private static bool _defaultAnimate = true;

        [PublicAPI]
        public static bool DefaultAnimate
        {
            get { return _defaultAnimate; }
            set { _defaultAnimate = value; }
        }

        private readonly HtmlTable _htmlTable;
        private readonly int _headerRowCount;
        private readonly int _footerRowCount;

        protected Table(HtmlTable htmlTable, int headerRowCount, int footerRowCount)
        {
            Animate = _defaultAnimate;
            _htmlTable = htmlTable;
            _headerRowCount = headerRowCount;
            _footerRowCount = footerRowCount;
        }

        protected HtmlTable HtmlTable
        {
            get { return _htmlTable; }
        }

        protected HtmlCell[] GetHtmlCellsForBodyRow(int bodyRowIndex)
        {
            if (bodyRowIndex < 0)
            {
                throw new ArgumentOutOfRangeException("bodyRowIndex", bodyRowIndex, "Must be greater than or equal to zero.");
            }
            if (bodyRowIndex >= RowCount)
            {
                throw new ArgumentOutOfRangeException("bodyRowIndex", bodyRowIndex, string.Format("Must be less than RowCount '{0}'", RowCount));
            }
            var tableRowIndex = bodyRowIndex + _headerRowCount;
            var row = (HtmlRow)_htmlTable.GetRow(tableRowIndex);
            return row.Cells.Cast<HtmlCell>().ToArray();
        }

        [PublicAPI]
        public int RowCount
        {
            get
            {
                return _htmlTable.RowCount - _headerRowCount - _footerRowCount;
            }
        }

        [PublicAPI]
        public bool Animate { get; set; }
    }
}
