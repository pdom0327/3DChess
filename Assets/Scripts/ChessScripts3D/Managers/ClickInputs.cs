using ChessScripts3D.BoardScrips;
using ChessScripts3D.InputSystem;
using ChessScripts3D.PieceScripts;
using ChessScripts3D.Socket;
using UnityEngine;
using UnityEngine.InputSystem;
using WebSocketSharp;
using Action = ChessScripts3D.Socket.Action;

namespace ChessScripts3D.Managers
{
    public class ClickInputs : SingleTon<ClickInputs>
    {
        public LayerMask pieceMask;
        public LayerMask cellMask;
        
        private InputFeedback _input;
    
        public Piece3D _clickedPiece;
        public BoardCell3D _clickedCell;

        void Awake()
        {
            _input = InputFeedback.Instance;

            _input.clicked += () =>
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                mousePos.z = 100f;

                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (!_clickedPiece && !_clickedCell)
                {
                    if (!Physics.Raycast(ray, out hit, mousePos.z, pieceMask)) return;
                    _clickedPiece = hit.collider.GetComponent<Piece3D>();
                    _clickedPiece.CheckIsClick(true);
                }
                else if (_clickedPiece && !_clickedCell)
                {
                    if (!Physics.Raycast(ray, out hit, mousePos.z, cellMask)) return;
                    _clickedCell = hit.collider.GetComponent<BoardCell3D>();
                    Send();
                    _clickedPiece.CheckIsClick(false);
                    _clickedPiece = null;
                    _clickedCell = null;
                }
            };
        }

        private void Send()
        {
            var piece = _clickedPiece.pieceData3D;
            var cell = _clickedCell;
            
            var data = new PieceMove()
            {
                action = Action.Temp.ToString(),
                pieceType = piece.PieceType.ToString(),
            
                currentRank = piece.Rank.ToString(),
                currentFile = piece.File.ToString(),
                currentLevel = piece.Level.ToString(),
            
                toMoveRank = cell.Rank.ToString(),
                toMoveFile = cell.File.ToString(),
                toMoveLevel = cell.Level.ToString()
            };
            
            var json = JsonUtility.ToJson(data);
            
            SocketRequest.Instance.WsRequest.Send(json);
        }
    }
}
