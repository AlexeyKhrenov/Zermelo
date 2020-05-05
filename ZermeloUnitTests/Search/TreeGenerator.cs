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
            var randomTree = GenerateTree(20);
            File.WriteAllText("../../../RandomByteTree.txt", randomTree);
        }

        public static ByteNode ReadTree()
        {
            var randomTree = File.ReadAllText("RandomByteTree.txt");
            return ParseByteTree(randomTree, 4);
        }

        public static string GenerateTree(int depth)
        {
            var rand = new Random();
            var builder = new StringBuilder();
            
            while (depth >= 0)
            {
                var buffer = new byte[(int) Math.Pow(2, depth)];
                rand.NextBytes(buffer);

                builder.Append(buffer[0]);
                for (var i = 1; i < buffer.Length; i++)
                {
                    builder.Append(' ');
                    builder.Append((sbyte)buffer[i]);
                }

                depth--;
            }
            
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
    }
}
