using CheckersAI.Evaluators;
using CheckersAI.Tree;
using CheckersAI.TreeSearch;
using System;
using System.IO;

namespace Benchmarking
{
    public class Evaluator : IEvaluator<sbyte>
    {
        public sbyte Evaluate(object board)
        {
            return (sbyte)((Node<int?>)board).Value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GenerateTree();

            var randomTree = File.ReadAllText("../../../RandomByteTree.txt");
            var tree = TreeGenerator.ParseTree(randomTree, 4);
        }

        private static void GenerateTree()
        {
            var randomTree = TreeGenerator.GenerateTree(1024 * 1024);
            File.WriteAllText("../../../RandomByteTree.txt", randomTree);
        }

        static Node<int?> CreateSimpleTree()
        {
            return new Node<int?>
            (
                null,
                true,
                new Node<int?>
                (
                    null,
                    false,
                    new Node<int?>(2, true),
                    new Node<int?>(7, true)
                ),
                new Node<int?>
                (
                    null,
                    false,
                    new Node<int?>(1, true),
                    new Node<int?>(8, true)
                )
            );
        }

        static Node<int?> CreateSampleTree()
        {
            return new Node<int?>
            (
                null,
                true,
                new Node<int?>
                (
                    null,
                    false,
                    new Node<int?>
                    (
                        null,
                        true,
                        new Node<int?>(-1, false),
                        new Node<int?>(3, false)
                    ),
                    new Node<int?>
                    (
                        null,
                        true,
                        new Node<int?>(5, false),
                        new Node<int?>(1, false)
                    )
                ),
                new Node<int?>
                (
                    null,
                    false,
                    new Node<int?>
                    (
                        null,
                        true,
                        new Node<int?>(-6, false),
                        new Node<int?>(-4, false)
                    ),
                    new Node<int?>
                    (
                        null,
                        true,
                        new Node<int?>(0, false),
                        new Node<int?>(9, false)
                    )
                )
            );
        }
    }
}
