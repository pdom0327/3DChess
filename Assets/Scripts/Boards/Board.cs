using System.Collections.Generic;
using UnityEngine;

namespace Boards
{
    public class Board : MonoBehaviour
    {
        public List<GameObject> boardCells;

        public GameObject GetCell(int x, int y)
        {
            foreach (var cell in boardCells)
            {
                if (cell.GetComponent<BoardCell>().cellPosition == new Vector2(x, y))
                {
                    return cell;
                }
            }
            return null;
        }
    }
}
