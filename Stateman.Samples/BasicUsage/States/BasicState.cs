using Stateman;

namespace BasicUsage.States
{
    abstract class BasicState : State
    {
        public BasicState(int readOnlyValue) => ReadOnlyValue = readOnlyValue;
        public int Value { get; set; }
        public int ReadOnlyValue { get; }
    }
}