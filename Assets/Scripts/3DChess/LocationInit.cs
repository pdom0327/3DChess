using System;

namespace _3DChess
{
    [Serializable]
    public enum PieceType
    {
        Pawn,
        Bishop,
        knight,
        Rook,
        Queen,
        King,
    }
    [Serializable]
    public enum Level
    {
        QL1 = 0,
        QL2,
        QL3,
        QL4,
        QL5,
        QL6,

        White,
        Neutral,
        Black,
    
        KL1,
        KL2,
        KL3,
        KL4,
        KL5,
        KL6,
    }
    [Serializable]
    public enum File
    {
        Z = 0,
        A,
        B,
        C,
        D,
        E,
    }
    [Serializable]
    public enum Rank
    {
        Zero = 0,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
    }
}