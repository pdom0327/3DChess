using System;
using System.Collections.Generic;
using ChessScripts3D.Managers;
using ChessScripts3D.Socket;
using UnityEngine;

namespace ChessScripts3D.PieceScripts
{
    public class PieceInit3D : MonoBehaviour
    {
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

        public void SetClickMask(string color)
        {
            if (color == "Black") ClickInputs.Instance.pieceMask = LayerMask.GetMask("BlackPiece");
            if (color == "White") ClickInputs.Instance.pieceMask = LayerMask.GetMask("WhitePiece");   
        }
        
        /*public void InitPiece(List<InitData> initList, PieceDataBase3D dataBase3D)
        {
            foreach (var data in initList)
            {
                Piece3D piece = null;
                
                if (data.rank is "Seven" or "Eight" or "Nine")
                    piece = checkBlackPieceType(Enum.Parse<PieceType>(data.pieceType));
                else
                    piece = checkWhitePieceType(Enum.Parse<PieceType>(data.pieceType));

                if (piece)
                {
                    PieceData3D pieceData = piece.pieceData3D;
                    pieceData.File = Enum.Parse<File>(data.file);
                    pieceData.Rank = Enum.Parse<Rank>(data.rank);
                    pieceData.Level = Enum.Parse<Level>(data.level);
            
                    //var boardCellList = BoardManager3D.Instance.board.boardCellList;
                    /*foreach (var cell in boardCellList)
                    {
                        if (pieceData.File == cell.File &&
                            pieceData.Rank == cell.Rank &&
                            pieceData.Level == cell.Level)
                        {
                            piece.transform.position = cell.transform.position;
                        }
                    }#1#
                    dataBase3D.pieceList.Add(piece);
                }
            }
        }
        private Piece3D checkWhitePieceType(PieceType type)
        {
            GameObject piece = null;
            
            switch (type)
            {
                case PieceType.Pawn:
                    piece = Instantiate(whitePawn);
                    break;
                case PieceType.Bishop:
                    piece = Instantiate(whiteBishop);
                    break;
                case PieceType.Knight:
                    piece = Instantiate(whiteKnight);
                    break;
                case PieceType.Rook:
                    piece = Instantiate(whiteRook);
                    break;
                case PieceType.Queen:
                    piece = Instantiate(whiteQueen);
                    break;
                case PieceType.King:
                    piece = Instantiate(whiteKing);
                    break;
            }

            if (piece) return piece.GetComponent<Piece3D>();
            
            print("피스가 존재하지 않습니다.");
            return null;
        }
        private Piece3D checkBlackPieceType(PieceType type)
        {
            GameObject piece = null;

            switch (type)
            {
                case PieceType.Pawn:
                    piece = Instantiate(blackPawn);
                    break;
                case PieceType.Bishop:
                    piece = Instantiate(blackBishop);
                    break;
                case PieceType.Knight:
                    piece = Instantiate(blackKnight);
                    break;
                case PieceType.Rook:
                    piece = Instantiate(blackRook);
                    break;
                case PieceType.Queen:
                    piece = Instantiate(blackQueen);
                    break;
                case PieceType.King:
                    piece = Instantiate(blackKing);
                    break;
            }
            
            if (piece) return piece.GetComponent<Piece3D>();
            
            print("피스가 존재하지 않습니다.");
            return null;
        }*/
    }
}