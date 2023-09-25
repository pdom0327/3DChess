using System;
using UnityEngine;

namespace Piece
{
    [Serializable]
    public class Piece : MonoBehaviour
    {
        public int id;
        public int x;
        public int y;
        public bool hasMoved;
        public string pieceColor;
        
        public void InitPiece(int id, int x, int y, string color)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.pieceColor = color;
        }
    }
}
