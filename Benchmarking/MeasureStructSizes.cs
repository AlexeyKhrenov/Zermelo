using Checkers.Minifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Benchmarking
{
    public static class MeasureStructSizes
    {
        private const string _filePath = "../../../StructSizes.txt";

        public static void Measure()
        {
            var rslt = new string[]
            {
                MeasureSize<PieceMinified>(),
                MeasureSize<BoardMinified>(),
                Environment.NewLine
            };
            File.AppendAllLines(_filePath, rslt);
        }

        private static string MeasureSize<T>() where T : struct
        {
            return $"{typeof(T).Name}: {Marshal.SizeOf(typeof(T))}";
        }
    }
}
