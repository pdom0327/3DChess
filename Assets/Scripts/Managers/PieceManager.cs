using System.Collections.Generic;
using Boards;
using Newtonsoft.Json;
using Pieces;
using UnityEngine;

namespace Managers
{
    public enum PieceState
    {
        Clicked,
        Active,
        Sleep,
    }
    
    public class PieceManager : MonoBehaviour
    {
        [Header("검정 피스들")]
        public Transform blackPieces;
        [Header("흰색 피스들")]
        public Transform whitePieces;
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
        [Space] 
        private static GameObject _piece;
        
        private static PieceManager _instance;
        public static PieceManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (PieceManager)FindObjectOfType(typeof(PieceManager));
                }
                return _instance;
            }
        }

        private List<Piece> _pieces;
        
        public void GetJsonPieceList(string jsonText)
        {
            var pieces = JsonConvert.DeserializeObject<List<PieceData>>(jsonText);
            
            foreach (var piece in pieces)
            {
                InitPieceById(piece.id, piece.x, piece.y);
            }
        }
        
        private void InitPieceById(int id, int x, int y)
        {
            Vector3 cellPos;
            string color;
            BoardManager boardManager = BoardManager.Instance;
            BoardCell cell; 
            
            if (id <= 16)
            {
                color = "black";
                cell = boardManager.board.boardCells[id - 1]; 
                cellPos = cell.transform.position;
            }
            else
            {
                color = "white";
                cell = boardManager.board.boardCells[31 + id];
                cellPos = cell.transform.position;
            }
            
            switch(id)
           {
               case 1: case 8: case 25: case 32:
                   MakePiece(whiteRook, blackRook, cell, cellPos, id, x, y, color);
                   break;
               case 2: case 7: case 26: case 31:
                   MakePiece(whiteKnight, blackKnight, cell, cellPos, id, x, y, color);
                   break;
               case 3: case 6: case 27: case 30:
                   MakePiece(whiteBishop, blackBishop, cell, cellPos, id, x, y, color);
                   break;
               case 4: case 28:
                   MakePiece(whiteQueen, blackQueen, cell, cellPos, id, x, y, color);
                   break;
               case 5: case 29:
                   MakePiece(whiteKing, blackKing, cell, cellPos, id, x, y, color);
                   break;
               case 9: case 10: case 11: case 12: case 13: case 14: case 15: case 16:
                   case 17: case 18: case 19: case 20: case 21: case 22: case 23: case 24:
                   MakePiece(whitePawn, blackPawn, cell, cellPos, id, x, y, color);
                   break;
           }
        }
        
        private void MakePiece(GameObject whitePiece, GameObject blackPiece, BoardCell cell, Vector3 cellPos, int id, int x, int y, string color)
        {
            if (color == "white")
                _piece = Instantiate(whitePiece, cellPos, Quaternion.Euler(whitePiece.transform.eulerAngles), whitePieces);
            else if (color == "black")
                _piece = Instantiate(blackPiece, cellPos, Quaternion.Euler(blackPiece.transform.eulerAngles), blackPieces);

            _piece.GetComponent<Piece>().InitPiece(id, x, y, color);
            
            cell.piece = _piece;
        }

        public void MovePiece(Collider cell)
        {
            // 피스가있던 자리 null
            // 갈 자리 채우기
            // 피스 x,y 바꾸기
            //GameManager.Instance.clickedPiece.pieceData.
            
            if (cell.GetComponent<BoardCell>().canMove)
            {
                foreach (var originCell in BoardManager.Instance.board.boardCells)
                {
                    if (originCell.cellPosition == new Vector2(
                            GameManager.Instance.clickedPiece.pieceData.x, GameManager.Instance.clickedPiece.pieceData.y))
                    {
                        originCell.piece = null;
                    }
                }
                
                GameManager.Instance.clickedPiece.transform.position = cell.transform.position;
                GameManager.Instance.clickedPiece.pieceData.x = cell.GetComponent<BoardCell>().cellPosition.x;
                GameManager.Instance.clickedPiece.pieceData.y = cell.GetComponent<BoardCell>().cellPosition.y;

                cell.GetComponent<BoardCell>().piece = GameManager.Instance.clickedPiece.gameObject;
            }
        }
    }
}