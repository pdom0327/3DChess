using System;
using UnityEngine;

namespace _3DChess.BoardScrips
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
