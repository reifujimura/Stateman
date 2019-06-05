using Stateman;

namespace BasicUsage.States
{
    class BasicState2 : BasicState
    {
        public BasicState2() : base(2)
        {
        }

        public int UniqueValue { get; set; }

        public override string ToString() => $"BasicState2 {{ Value: {Value}, ReadOnlyValue: {ReadOnlyValue}, UniqueValue: {UniqueValue} }}";
    }
}