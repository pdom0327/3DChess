using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boards
{
    public class Board : MonoBehaviour
    {
        public List<BoardCell> boardCells;

        public BoardCell GetCell(int x, int y)
        {
            foreach (var cell in boardCells)
            {
                if (cell.cellPosition == new Vector2(x, y))
                {
                    cell.canMove = true;
                    return cell;
                }
            }
            return null;
        }
        
        
    }
}
