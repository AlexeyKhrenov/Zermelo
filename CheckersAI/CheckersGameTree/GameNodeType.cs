﻿using System;

namespace CheckersAI.CheckersGameTree
{
    [Flags]
    internal enum GameNodeType
    {
        IsEvaluated = 1,
        WasCutOff = 2,
        IsMaxPlayer = 4,
        IsFinalized = 8
    }
}