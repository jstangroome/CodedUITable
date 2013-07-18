using System;
using CodedUITable;

namespace DemoWeb.CodedUITests.Pages.DefaultClasses
{
    public partial class Default
    {
        public class TheTableRow : TableRow
        {
            [Column(0)]
            public string Name { get { return GetColumnValue(); } }

            [Column(1)]
            public DateTime DateOfBirth
            {
                get
                {
                    return DateTime.Parse(GetColumnValue());
                }
            }

            [Column(2)]
            public int Age
            {
                get
                {
                    return int.Parse(GetColumnValue());
                }
            }
        }

        private readonly Lazy<Table<TheTableRow>> _theTable;

        public Default()
        {
            _theTable = new Lazy<Table<TheTableRow>>(() => new Table<TheTableRow>(UIDemoWebWindowsInternWindow.UIDemoWebDocument.UITheTable));
        }

    
    }
}
