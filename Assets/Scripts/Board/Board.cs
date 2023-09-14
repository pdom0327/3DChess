using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject boardCell;
    private float _cellSize = 1.5f;
    private float _borderLength = 5.25f;

    void Start()
    {
        CreateGrid();
        for (int i = 0; i < 64; i++)
        {
            Debug.Log(BoardManager.Instance.boardCells[i].x + ", " + BoardManager.Instance.boardCells[i].y);    
        }
    }

    public void CreateGrid()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++)
            {
                Vector3 cellPosition = new Vector3(x * _cellSize - _borderLength, 0.17f, z * _cellSize - _borderLength) ;
                
                CreateCell(cellPosition, x, z);
            }
        }
    }

    private void CreateCell(Vector3 position, int x, int y)
    {
        GameObject cell = Instantiate(boardCell, position, Quaternion.Euler(transform.eulerAngles), transform);
        cell.GetComponent<BoardCell>().x = x;
        cell.GetComponent<BoardCell>().y = y;

        BoardManager.Instance.boardCells.Add(cell.GetComponent<BoardCell>());
    }
}
