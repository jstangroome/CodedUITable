namespace CodedUITable
{
    public class ColumnMetadata
    {
        public ColumnMetadata(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; private set; }
        public string Name { get; private set; }
    }
}