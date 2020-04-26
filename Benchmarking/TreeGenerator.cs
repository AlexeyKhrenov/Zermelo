using CheckersAI.ByteTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Benchmarking
{
    internal static class TreeGenerator
    {
        public static void GenerateTree()
        {
            var randomTree = GenerateTree(1024 * 1024);
            File.WriteAllText("../../../RandomByteTree.txt", randomTree);
        }

        public static ByteNode ReadTree()
        {
            var randomTree = File.ReadAllText("RandomByteTree.txt");
            return ParseByteTree(randomTree, 4);
        }

        public static AlfaBetaByteNode ReadAlfaBetaByteTree()
        {
            var randomTree = File.ReadAllText("RandomByteTree.txt");
            return ParseAsAlfaBetaTree(randomTree, 4);
        }

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
                builder.Append((sbyte)buffer[i]);
            }

            return builder.ToString();
        }

        public static ByteNode ParseByteTree(string tree, byte branchingFactor)
        {
            var bytes = tree.Split(' ');

            var depth = Math.Log(bytes.Length, branchingFactor);

            if (depth % 1 != 0)
            {
                throw new ArgumentException("Invalid branching factor");
            }

            var isMaxPlayer = depth % 2 == 0;

            var queue = new Queue<ByteNode>();

            foreach (var b in bytes)
            {
                queue.Enqueue(new ByteNode(sbyte.Parse(b), isMaxPlayer));
            }

            while (queue.Count != 1)
            {
                var nodes = new ByteNode[branchingFactor];

                for (var i = 0; i < branchingFactor; i++)
                {
                    nodes[i] = queue.Dequeue();
                }

                queue.Enqueue(new ByteNode(0, !nodes[0].IsMaxPlayer, nodes));
            }

            return queue.Dequeue();
        }

        public static AlfaBetaByteNode ParseAsAlfaBetaTree(string tree, byte branchingFactor)
        {
            var bytes = tree.Split(' ');

            var depth = Math.Log(bytes.Length, branchingFactor);

            if (depth % 1 != 0)
            {
                throw new ArgumentException("Invalid branching factor");
            }

            var isMaxPlayer = depth % 2 == 0;

            var queue = new Queue<AlfaBetaByteNode>();

            foreach (var b in bytes)
            {
                queue.Enqueue(new AlfaBetaByteNode(sbyte.Parse(b), isMaxPlayer));
            }

            while (queue.Count != 1)
            {
                var nodes = new AlfaBetaByteNode[branchingFactor];

                for (var i = 0; i < branchingFactor; i++)
                {
                    nodes[i] = queue.Dequeue();
                }

                var newNode = new AlfaBetaByteNode(0, !nodes[0].IsMaxPlayer, nodes);
                newNode.LinkBackChildren();
                queue.Enqueue(newNode);
            }

            var result = queue.Dequeue();
            result.EnumerateDepth();
            return result;
        }
    }
}
