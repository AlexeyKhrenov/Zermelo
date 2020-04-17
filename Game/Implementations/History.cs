using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Game.Implementations
{
    public class History : IHistory
    {
        private Stack<IHistoryItem> _stack;

        public History()
        {
            _stack = new Stack<IHistoryItem>();
        }

        public IHistoryItem Pop()
        {
            return _stack.Pop();
        }

        public void Push(IHistoryItem item)
        {
            _stack.Push(item);
        }
    }
}
