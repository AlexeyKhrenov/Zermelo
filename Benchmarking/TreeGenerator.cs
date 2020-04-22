using CheckersAI.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarking
{
    internal static class TreeGenerator
    {
        /// <summary>
        /// generates random byte tree of equal deep
        /// </summary>
        public static string GenerateTree(int size)
        {
            var rand = new Random();
            var buffer = new byte[size];
            rand.NextBytes(buffer);

            var builder = new StringBuilder();
            builder.Append(buffer[0]);

            for (var i = 1; i < buffer.Length; i++)
            {
                builder.Append(' ');
                builder.Append(buffer[i]);
            }

            return builder.ToString();
        }

        public static Node<byte> ParseTree(string tree, byte branchingFactor)
        {
            var bytes = tree.Split(' ');

            if (Math.Log(bytes.Length, branchingFactor) % 1 != 0)
            {
                throw new ArgumentException("Invalid branching factor");
            }

            var queue = new Queue<Node<byte>>();

            foreach (var b in bytes)
            {
                queue.Enqueue(new Node<byte>(byte.Parse(b), false));
            }

            while (queue.Count != 1)
            {
                var nodes = new Node<byte>[branchingFactor];

                for (var i = 0; i < branchingFactor; i++)
                {
                    nodes[i] = queue.Dequeue();
                }

                queue.Enqueue(new Node<byte>(0, nodes[0].IsMaxPlayer, nodes));
            }

            return queue.Dequeue();
        }
    }
}
