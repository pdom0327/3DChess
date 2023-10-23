using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boards
{
    public class BoardCell : MonoBehaviour
    {
        public GameObject piece;
        public Vector2Int cellPosition;
        [Space]
        public bool canMove;
    }    
}

