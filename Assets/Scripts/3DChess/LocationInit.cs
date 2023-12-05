namespace _3DChess
{
    public enum PieceType
    {
        Pawn,
        Bishop,
        knight,
        Rook,
        Queen,
        King,
    }
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
    public enum File
    {
        Z = 0,
        A,
        B,
        C,
        D,
        E,
    }
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