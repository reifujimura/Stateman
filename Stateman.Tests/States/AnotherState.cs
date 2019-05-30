using System;

namespace Stateman.Tests.TestStates
{
    public class AnotherState : State
    {
        public int Value { get; set; }
        public long ReadOnlyValue { get; } = DateTime.Now.Ticks;
        public long GetPrivateValue() => PrivateValue;
        private long PrivateValue { get; set; } = DateTime.Now.Ticks;
    }
}