using System;

namespace _3DChess
{
    [Serializable]
    public class InitData
    {
        public PieceType pieceType;
        public Rank rank;
        public File file;
        public Level level;
    }
}
