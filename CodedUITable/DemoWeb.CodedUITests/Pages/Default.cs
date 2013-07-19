using System;
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
