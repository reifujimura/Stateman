using Stateman;

namespace BasicUsage.States
{
    class BasicState1 : BasicState
    {
        public BasicState1() : base(1)
        {
        }

        public int UniqueValue { get; set; }

        public override string ToString() => $"BasicState1 {{ Value: {Value}, ReadOnlyValue: {ReadOnlyValue}, UniqueValue: {UniqueValue} }}";
    }
}