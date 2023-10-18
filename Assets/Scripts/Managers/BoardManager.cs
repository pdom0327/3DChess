using System.Collections.Generic;
using Boards;
using UnityEngine;

namespace Managers
{
    public class BoardManager : MonoBehaviour
    {
        public Board board;
        
        public GameObject boardCell;

        private Board _board;

        private List<GameObject> _activatedCells;

        private static BoardManager _instance = null;

        public static BoardManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (BoardManager)FindObjectOfType(typeof(BoardManager));
                }
                return _instance;
            }
        }

        void Start()
        {
            _board ??= GameObject.FindGameObjectWithTag(nameof(Board)).GetComponent<Board>();

            CreateGrid();
        }
        
        private void CreateGrid()
        {
            var cellSize = 1.5f;
            var borderLength = 5.25f;
            
            for (int x = 0; x < 8; x++)
            {
                for (int z = 0; z < 8; z++)
                {
                    Vector3 cellPosition = new Vector3(x * cellSize - borderLength, 0.1f, z * cellSize - borderLength) ;
                
                    CreateCell(cellPosition, x, z);
                }
            }
        }

        private void CreateCell(Vector3 position, int x, int y)
        {
            GameObject cell = Instantiate(boardCell, position, Quaternion.Euler(transform.eulerAngles), board.transform);
            cell.GetComponent<BoardCell>().cellPosition = new Vector2(x, y);
            cell.SetActive(false);
            
            board.boardCells.Add(cell);
        }

        public void ActiveCell(List<Point> points)
        {
            foreach (var point in points)
            {
                var a = 1;
                var cell = board.GetCell(point.x, point.y);

                if (cell == null) return ;

                cell.SetActive(true);
                _activatedCells.Add(cell);
            }

            foreach (var a in _activatedCells)
            {
                Debug.Log(a);
            }
        }
    }
}
