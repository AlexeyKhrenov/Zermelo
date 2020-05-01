using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersAI.TreeSearch
{
    internal static class TreeExtensions
    {
        /// <summary>
        /// shows the depth of the left branch of the tree
        /// </summary>
        public static int GetDepth<TNode, TMetric>(this TNode root)
            where TNode : INode<TNode, TMetric>
            where TMetric : struct
        {
            if (root == null)
            {
                throw new ArgumentNullException();
            }

            var next = root;
            int count = 0;

            while (next.Children != null && next.Children.Length != 0)
            {
                next = next.Children[0];
                count++;
            }
            return count;
        }

        public static List<TNode> ToList<TNode, TMetric>(this TNode root)
            where TNode : INode<TNode, TMetric>
            where TMetric : struct
        {
            if (root == null)
            {
                throw new ArgumentNullException();
            }

            var list = new List<TNode>();
            var queue = new Queue<TNode>();
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                var next = queue.Dequeue();
                list.Add(next);
                if (next.Children != null)
                {
                    foreach (var child in next.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return list;
        }
    }
}
