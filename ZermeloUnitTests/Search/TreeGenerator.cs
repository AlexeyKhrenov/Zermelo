using CheckersAI.ByteTree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Benchmarking")]
namespace ZermeloUnitTests.Search
{
    internal static class TreeGenerator
    {
        public static void GenerateTree()
        {
            var randomTree = GenerateTree(10, 4);
            File.WriteAllText("../../../RandomByteTree.txt", randomTree);
        }

        public static ByteNode ReadTree()
        {
            var randomTree = File.ReadAllText("RandomByteTree.txt");
            return ParseByteTree(randomTree, 4);
        }

        public static AlfaBetaByteNode ReadAlfaBetaTree()
        {
            var randomTree = File.ReadAllText("RandomByteTree.txt");
            return ParseAlfaBetaTree(randomTree, 4);
        }

        public static string GenerateTree(int depth, int branchingFactor)
        {
            var rand = new Random();
            var builder = new StringBuilder();

            while (depth >= 0)
            {
                var buffer = new byte[(int) Math.Pow(branchingFactor, depth)];
                rand.NextBytes(buffer);

                for (var i = 0; i < buffer.Length; i++)
                {
                    builder.Append((sbyte)buffer[i]);
                    builder.Append(' ');
                }

                depth--;
            }

            builder.Remove(builder.Length - 1, 1);
            
            return builder.ToString();
        }

        public static ByteNode ParseByteTree(string tree, byte branchingFactor)
        {
            var bytes = tree.Split(' ');

            var depth = -1;
            var sum = 0;

            while (sum < bytes.Length)
            {
                depth++;
                sum += (int) Math.Pow(branchingFactor, depth);
            }

            if (sum != bytes.Length)
            {
                throw new ArgumentException("Tree of unequal depth");
            }
            
            var queue = new Queue<ByteNode>();
            var startDequeue = false;
            var bottomLevel = (int) Math.Pow(branchingFactor, depth);

            for (var i = bytes.Length - 1; i >= 0; i--)
            {
                ByteNode node;

                if (queue.Count == bottomLevel)
                {
                    startDequeue = true;
                }

                if (startDequeue)
                {
                    var children = new ByteNode[branchingFactor];
                    for (var j = 0; j < branchingFactor; j++)
                    {
                        children[j] = queue.Dequeue();
                    }
                    
                    node = new ByteNode(sbyte.Parse(bytes[i]), !children[0].IsMaxPlayer, children);
                }
                else
                {
                    var children = new ByteNode[0];
                    node = new ByteNode(sbyte.Parse(bytes[i]), depth % 2 == 0, children);
                }
                    
                queue.Enqueue(node);
            }

            return queue.Dequeue();
        }

        public static AlfaBetaByteNode ParseAlfaBetaTree(string tree, byte branchingFactor)
        {
            var bytes = tree.Split(' ');

            var depth = -1;
            var sum = 0;

            while (sum < bytes.Length)
            {
                depth++;
                sum += (int)Math.Pow(branchingFactor, depth);
            }

            if (sum != bytes.Length)
            {
                throw new ArgumentException("Tree of unequal depth");
            }

            var queue = new Queue<AlfaBetaByteNode>();
            var startDequeue = false;
            var bottomLevel = (int)Math.Pow(branchingFactor, depth);

            for (var i = bytes.Length - 1; i >= 0; i--)
            {
                AlfaBetaByteNode node;

                if (queue.Count == bottomLevel)
                {
                    startDequeue = true;
                }

                if (startDequeue)
                {
                    var children = new AlfaBetaByteNode[branchingFactor];
                    for (var j = 0; j < branchingFactor; j++)
                    {
                        children[j] = queue.Dequeue();
                    }

                    node = new AlfaBetaByteNode(sbyte.Parse(bytes[i]), !children[0].IsMaxPlayer, children);
                }
                else
                {
                    var children = new AlfaBetaByteNode[0];
                    node = new AlfaBetaByteNode(sbyte.Parse(bytes[i]), depth % 2 == 0, children);
                }

                queue.Enqueue(node);
            }

            return queue.Dequeue();
        }
    }
}
