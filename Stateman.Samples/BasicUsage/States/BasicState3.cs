using Stateman;

namespace BasicUsage.States
{
    class BasicState3 : State
    {
        public int Value { get; set; }
        public int ReadOnlyValue { get; }
        public int UniqueValue { get; set; }

        public override string ToString() => $"BasicState3 {{ Value: {Value}, ReadOnlyValue: {ReadOnlyValue}, UniqueValue: {UniqueValue} }}";
    }
}