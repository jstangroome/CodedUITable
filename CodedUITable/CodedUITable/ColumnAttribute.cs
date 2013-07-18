using System;

namespace CodedUITable
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(int columnIndex)
        {
            ColumnIndex = columnIndex;
        }

        public int ColumnIndex { get; private set; }
    }
}