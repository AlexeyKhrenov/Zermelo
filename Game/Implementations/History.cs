using Game.PublicInterfaces;
using System.Collections.Generic;

namespace Game.Implementations
{
    public class History : IHistory
    {
        private Stack<IHistoryItem> _stack;

        public int Length => _stack.Count;

        public History()
        {
            _stack = new Stack<IHistoryItem>();
        }

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
