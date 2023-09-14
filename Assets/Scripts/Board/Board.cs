using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject boardCell;
    private float _cellSize;
    private float _borderLength;

    void Start()
    {
        _cellSize = 1.5f;

        _borderLength = 5.25f;
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
        cell.GetComponent<BoardCell>().cellPosition = new Vector2(x, y);

        Managers.BoardManager.Instance.boardCells.Add(cell.GetComponent<BoardCell>());
    }
}
