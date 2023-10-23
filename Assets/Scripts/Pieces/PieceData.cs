using System;
using UnityEngine;

namespace Pieces
{
    [Serializable]
    public class PieceData
    { 
        public bool hasMoved = true;
        public int id;
        public int x;
        public int y;
    }
}