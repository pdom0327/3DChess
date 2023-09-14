using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject boardCell;
    private float _cellSize = 1.5f;
    private float _borderLength = 5.25f;
    private int[,,] _cellPoints = new int[8, 0, 8];
    private Vector3[] _boardVertices;
    
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int z = 0; z < 8; z++)
            {
                Vector3 cellPosition = new Vector3(x * _cellSize - _borderLength, 0f, z * _cellSize - _borderLength) ;
                
                CreateCell(cellPosition, x, z);
            }
        }
    }

    void CreateCell(Vector3 position, int x, int y)
    {
        GameObject cell = Instantiate(boardCell, position, Quaternion.Euler(transform.eulerAngles), transform);
        cell.GetComponent<BoardCell>().x = x;
        cell.GetComponent<BoardCell>().y = y;
    }
}
