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

        public List<GameObject> activatedCells;

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
                
                    CreateCell(cellPosition, z, x);
                }
            }
        }

        private void CreateCell(Vector3 position, int x, int y)
        {
            GameObject cell = Instantiate(boardCell, position, Quaternion.Euler(transform.eulerAngles), board.transform);

            cell.GetComponent<BoardCell>().cellPosition = new Vector2Int(x, y);
            cell.SetActive(false);
            
            board.boardCells.Add(cell.GetComponent<BoardCell>());
        }

        public void ActiveCell(List<Point> points)
        {
            foreach (var point in points)
            {
                var cell = board.GetCell(point.x, point.y);
                
                if (!cell) return ;

                if (cell.piece)
                {
                    cell.transform.localScale = new Vector3(cell.transform.localScale.x, 1f, cell.transform.localScale.z);
                    cell.transform.position = new Vector3(cell.transform.position.x, 1f, cell.transform.position.z);    
                }
                else
                {
                    cell.transform.localScale = new Vector3(cell.transform.localScale.x, 0.05f, cell.transform.localScale.z);
                    cell.transform.position = new Vector3(cell.transform.position.x, 0.1f, cell.transform.position.z);       
                }

                cell.canMove = true;
                cell.gameObject.SetActive(true);
                
                activatedCells.Add(cell.gameObject);
            }
        }

        public void ClearCell()
        {
            foreach (var cell in activatedCells)
            {
                cell.GetComponent<BoardCell>().canMove = false;
                cell.SetActive(false);
            }
            
            activatedCells.Clear();
        }
    }
}
