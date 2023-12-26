using System;

namespace ChessScripts3D.Socket
{
    [Serializable]
    public class PieceMove
    {
        public string action;
        public string pieceType;
        
        public string currentRank;
        public string currentFile;
        public string currentLevel;
        
        public string toMoveRank;
        public string toMoveFile;
        public string toMoveLevel;
    }
}
