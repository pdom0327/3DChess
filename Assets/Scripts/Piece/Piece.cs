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
    }
}
