using UnityEngine;

namespace ChessScripts3D.PieceScripts
{
    public class PieceMove3D : MonoBehaviour
    {
        /*private void MovePiece(PieceMove data)
        {
            var pieces = dataBase.pieceList;
            
            foreach (var piece in pieces)
            {
                var pieceData = piece.pieceData3D;
                if (Enum.Parse<PieceType>(data.pieceType) == pieceData.PieceType 
                    && Enum.Parse<Rank>(data.currentRank) == pieceData.Rank
                    && Enum.Parse<File>(data.currentFile) == pieceData.File
                    && Enum.Parse<Level>(data.currentLevel) == pieceData.Level)
                {
                    var cells = BoardManager3D.Instance.board.boardCellList;
                    foreach (var cell in cells)
                    {
                        if (Enum.Parse<Rank>(data.toMoveRank) == cell.Rank
                            && Enum.Parse<File>(data.toMoveFile) == cell.File
                            && Enum.Parse<Level>(data.toMoveLevel) == cell.Level)
                        {
                            var MovePos = cell.transform.position;
                            piece.transform.position = MovePos;
                        }
                    }
                }    
            }
        }*/
    }
}