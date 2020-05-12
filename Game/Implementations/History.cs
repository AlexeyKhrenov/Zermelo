using System.Collections.Generic;
using Game.PublicInterfaces;

namespace Game.Implementations
{
    public class History : IHistory
    {
        private readonly Stack<IHistoryItem> _stack;

        public History()
        {
            _stack = new Stack<IHistoryItem>();
        }

        public int Length => _stack.Count;

        public IHistoryItem Pop()
        {
            return _stack.Pop();
        }

        public IHistoryItem Latest => _stack.Count != 0 ? _stack.Peek() : null;

        public void Push(IHistoryItem item)
        {
            _stack.Push(item);
        }
    }
}