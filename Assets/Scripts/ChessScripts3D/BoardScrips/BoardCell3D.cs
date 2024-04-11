using System;
using UnityEngine;
using WarpSquareEngine;
using File = ChessScripts3D.Socket.File;
using Level = ChessScripts3D.Socket.Level;
using Rank = ChessScripts3D.Socket.Rank;

namespace ChessScripts3D.BoardScrips
{
    [Serializable]
    public class BoardCell3D : MonoBehaviour
    {
        public GameObject piece;
        public bool canMove;
        
        private Rank _rank;
        public Rank Rank
        {
            get => _rank;
            set => _rank = value;
        }
        private File _file;
        public File File
        {
            get => _file;
            set => _file = value;
        }
        private Level _level;
        public Level Level
        {
            get => _level;
            set => _level = value;
        }
    }
}
