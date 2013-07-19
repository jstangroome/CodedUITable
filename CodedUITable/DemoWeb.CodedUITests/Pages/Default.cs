using System;
using System.Collections.Generic;
using System.Linq;
using CodedUITable;

// ReSharper disable once CheckNamespace
namespace DemoWeb.CodedUITests.Pages.DefaultClasses
{
    public partial class Default
    {
        public class PersonRow : TableRow
        {
            [Column(0)]
            public string Name { get { return GetColumnValue(); } }

            [Column(1)]
            public DateTime DateOfBirth
            {
                get
                {
                    return DateTime.ParseExact(GetColumnValue(), "dd/MM/yyyy", null);
                }
            }

            // [Column(2)] // attribute not specified to demonstrate overriding GetColumnMetaData below
            public int Age
            {
                get
                {
                    return int.Parse(GetColumnValue());
                }
            }

            protected override ColumnMetadata GetDefaultColumn(ColumnMetadata[] columns)
            {
                // overriding this method is optional but changes the the column used when hovering or clicking a row.
                return columns.Single(c => c.Name == "DateOfBirth");
            }

            protected override ColumnMetadata[] GetColumnMetadata()
            {
                // overriding this method is optional but allows changes to the column data normally extracted from attributes.
                var columns = new List<ColumnMetadata>(base.GetColumnMetadata());

                columns.Add(new ColumnMetadata(2, "Age"));
                // more useful for dynamically changing columns based on other content detected on a page

                return columns.ToArray();
            }
        }

        private readonly Lazy<Table<PersonRow>> _theTable;

        public Default()
        {
            _theTable = new Lazy<Table<PersonRow>>(() => new Table<PersonRow>(UIDemoWebWindowsInternWindow.UIDemoWebDocument.UITheTable));
        }

        public PersonRow FindPersonRowByName(string name)
        {
            return _theTable.Value.FindRow(row => name.Equals(row.Name, StringComparison.OrdinalIgnoreCase));
        }

        public void AssertAllPersonsAreAdults()
        {
            _theTable.Value.AssertRowDoesNotExist(row => row.Age < 18, "All rows should have an Age of 18 or older.");
        }
    
    }
}
