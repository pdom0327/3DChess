using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    public class Point : MonoBehaviour
    {
        public static int x;
        public static int y;
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

        private List<Piece.Piece> _pieces;
        
        public void GetJsonPieceList(string jsonText)
        {
            _pieces = JsonConvert.DeserializeObject<List<Piece.Piece>>(jsonText);
            
            foreach (var piece in _pieces)
            {
                InitPieceById(piece.id, piece.x, piece.y);
            }
        }
        
        private void InitPieceById(int id, int x, int y)
        {
            Vector3 pos;
            GameObject piece ;
            string color;
            
            if (id <= 16)
            {
                color = "black";
                pos = BoardManager.Instance.boardCells[id - 1].transform.position;
            }
            else
            {
                color = "white";
                pos = BoardManager.Instance.boardCells[31 + id].transform.position;
            }
            
            switch(id)
           {
               // black
               case 1: case 8:
                   piece = Instantiate(blackRook, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 2: case 7:
                   piece = Instantiate(blackKnight, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 3: case 6:
                   piece = Instantiate(blackBishop, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 4:
                   piece = Instantiate(blackQueen, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 5:
                   piece = Instantiate(blackKing, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 9: case 10: case 11: case 12: case 13: case 14: case 15: case 16:
                   piece = Instantiate(blackPawn, pos, Quaternion.Euler(transform.eulerAngles), blackPieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               // white
               case 25: case 32:
                   piece = Instantiate(whiteRook, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 26: case 31:
                   piece = Instantiate(whiteKnight, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 27: case 30:
                   piece = Instantiate(whiteBishop, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 28:
                   piece = Instantiate(whiteQueen, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 29:
                   piece = Instantiate(whiteKing, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
               case 17: case 18: case 19: case 20: case 21: case 22: case 23: case 24:
                   piece = Instantiate(whitePawn, pos, Quaternion.Euler(transform.eulerAngles), whitePieces);
                   piece.GetComponent<Piece.Piece>().InitPiece(id, x, y, color);
                   break;
           }
        }
    }
}