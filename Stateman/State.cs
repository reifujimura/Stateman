using System;
using System.Linq;

namespace Stateman
{
    public abstract class State
    {
        public TState Generate<TState>() where TState : State, new()
        {
            var nextState = new TState();
            var type = this.GetType();
            var props = typeof(TState).GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite && type.IsSubclassOf(prop.DeclaringType))
                {
                    var value = prop.GetValue(this);
                    if (value == null) continue;
                    prop.SetValue(nextState, value);
                }
            }
            return nextState;
        }
    }
}