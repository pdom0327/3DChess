using System;

namespace ChessScripts3D.Socket
{
    [Serializable]
    public class InitData
    {
        public string pieceType;
        public string rank;
        public string file;
        public string level;
    }
}
