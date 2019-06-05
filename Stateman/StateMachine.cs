using System;
using System.Collections.Generic;
using System.Threading;

namespace Stateman
{
    public class StateMachine
    {
        public event Action<StateMachine> Transited;
        private readonly ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        private Stack<State> previous = new Stack<State>();
        private Stack<State> next = new Stack<State>();
        private State state;
        public State State
        {
            get
            {
                readerWriterLock.EnterReadLock();
                var value = state;
                readerWriterLock.ExitReadLock();
                return value;
            }
        }

        public StateMachine(State defaultState)
        {
            if (defaultState == null)
            {
                throw new NullReferenceException();
            }
            state = defaultState;
        }
        public void Transit<TStateFrom, TStateTo>() where TStateFrom : State where TStateTo : State, new()
        {
            readerWriterLock.EnterWriteLock();
            if (state is TStateFrom)
            {
                previous.Push(state);
                state = state.Generate<TStateTo>();
                next.Clear();
                readerWriterLock.ExitWriteLock();
                Transited?.Invoke(this);
            }
            else
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public void Previous()
        {
            if (previous.Count == 0) return;
            readerWriterLock.EnterWriteLock();
            next.Push(state);
            state = previous.Pop();
            readerWriterLock.ExitWriteLock();
            Transited?.Invoke(this);
        }

        public void Next()
        {
            if (next.Count == 0) return;
            readerWriterLock.EnterWriteLock();
            previous.Push(state);
            state = next.Pop();
            readerWriterLock.ExitWriteLock();
            Transited?.Invoke(this);
        }

        public void ClearHistory()
        {
            readerWriterLock.EnterWriteLock();
            previous.Clear();
            next.Clear();
            readerWriterLock.ExitWriteLock();
        }
    }
}
