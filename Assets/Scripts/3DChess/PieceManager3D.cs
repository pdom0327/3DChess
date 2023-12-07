using System;
using System.Collections;
using System.Collections.Generic;
using _3DChess;
using UnityEngine;

public class PieceManager3D : MonoBehaviour
{
    public string color;

    private static GameObject _piece;
    
    [Header("검정 피스")][Space]
    public GameObject blackPawn;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackQueen;
    public GameObject blackKing;
    public GameObject blackRook;
    [Header("흰색 피스")][Space]
    public GameObject whitePawn;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteQueen;
    public GameObject whiteKing;
    public GameObject whiteRook;
    
    private static PieceManager3D _instance;

    public static PieceManager3D Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 중복 방지를 위해 새로 생성된 객체 파괴
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPiece(List<InitData> initList)
    {
        foreach (var data in initList)
        {
            var piece = checkPieceType(data.pieceType);
            var pieceData = piece.pieceData3D;
            pieceData.File = data.file;
            pieceData.Rank = data.rank;
            pieceData.Level = data.level;
            
            var boardCellList = BoardManager3D.Instance.board.boardCellList;
            foreach (var cell in boardCellList)
            {
                if (pieceData.File == cell.File &&
                    pieceData.Rank == cell.Rank &&
                    pieceData.Level == cell.Level)
                {
                    piece.transform.position = cell.transform.position;
                }
            }
        }
    }

    private Piece3D checkPieceType(PieceType type)
    {
        if (color is "Black")
        {
            switch (type)
            {
                case PieceType.Pawn:
                    StartCoroutine(AsyncInstance(blackPawn));
                    break;
                case PieceType.Bishop:
                    StartCoroutine(AsyncInstance(blackBishop));
                    break;
                case PieceType.knight:
                    StartCoroutine(AsyncInstance(blackKnight));
                    break;
                case PieceType.Rook:
                    StartCoroutine(AsyncInstance(blackRook));
                    break;
                case PieceType.Queen:
                    StartCoroutine(AsyncInstance(blackQueen));
                    break;
                case PieceType.King:
                    StartCoroutine(AsyncInstance(blackKing));
                    break;
            }
        }
        else if (color is "White")
        {
            switch (type)
            {
                case PieceType.Pawn:
                    StartCoroutine(AsyncInstance(whitePawn));
                    break;
                case PieceType.Bishop:
                    StartCoroutine(AsyncInstance(whiteBishop));
                    break;
                case PieceType.knight:
                    StartCoroutine(AsyncInstance(whiteKnight));
                    break;
                case PieceType.Rook:
                    StartCoroutine(AsyncInstance(whiteRook));
                    break;
                case PieceType.Queen:
                    StartCoroutine(AsyncInstance(whiteQueen));
                    break;
                case PieceType.King:
                    StartCoroutine(AsyncInstance(whiteKing));
                    break;
            }
        }
        return _piece.GetComponent<Piece3D>();
    }
    private IEnumerator AsyncInstance(GameObject original)
    {
        _piece = Instantiate(original);
        yield return null;
    } 
}
