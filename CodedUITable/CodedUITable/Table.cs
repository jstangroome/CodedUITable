using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CodedUITable
{
    public abstract class Table
    {
        public static bool DefaultAnimate { get; set; }

        Table()
        {
            DefaultAnimate = true;
        }

        private readonly HtmlTable _htmlTable;
        private readonly int _headerRowCount;
        private readonly int _footerRowCount;

        protected Table(HtmlTable htmlTable, int headerRowCount, int footerRowCount)
        {
            Animate = DefaultAnimate;
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

        public int RowCount
        {
            get
            {
                return _htmlTable.RowCount - _headerRowCount - _footerRowCount;
            }
        }

        public bool Animate { get; set; }
    }
}
