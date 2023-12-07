using System.Collections;
using System.Collections.Generic;
using _3DChess.BoardScrips;
using UnityEngine;

public class BoardManager3D : MonoBehaviour
{

    public Board3D board;

    private static BoardManager3D _instance;
    
    public static BoardManager3D Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = (BoardManager3D)FindObjectOfType(typeof(BoardManager3D));
            }
            return _instance;
        }
    }
    
    void Start()
    {
        board.MainBoardInit();
        board.AttackBoardInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
