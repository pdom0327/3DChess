using ChessScripts3D.Managers;
using UnityEngine;
using WebSocketSharp;

namespace ChessScripts3D.Socket
{
    public class SocketRequest : SingleTon<SocketRequest>
    {
        // todo 지워
        public Canvas canvas;
        
        public string url;
        private PieceManager3D _pieceManager;
        private DeleteMe _deleteMe;
        public WebSocket WsRequest { get; private set; }

        private void Awake()
        {
            _pieceManager = PieceManager3D.Instance;
            _deleteMe = FindObjectOfType<DeleteMe>();
            
            WsRequest = new WebSocket(url);
            
            WsRequest.OnOpen += (sender, e) => { };

            WsRequest.OnMessage += (sender, e) =>
            {
                Ws_Init_OnMessage(sender, e);
                Ws_InGame_OnMessage(sender, e);
            };
            
            WsRequest.OnClose += (sender, e) => {
                Debug.Log("[WebSocket Close (" + e.Code + ")] " + e.Reason);
                if (_deleteMe != null)
                {
                    _deleteMe.a = true;
                }
            };
            
            WsRequest.OnError += (sender, e) => {
                Debug.LogError("[WebSocket Error] " + e.Message);
                if (e.Exception != null) {
                    Debug.LogError("Exception: " + e.Exception.ToString());
                }
            };
            
            WsRequest.Connect();
        }

        private void Ws_Init_OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.Data.Contains("action")) return;
            
            if (e.Data.Contains(Action.COLOR.ToString()))
            {
                var data = JsonUtility.FromJson<GetActionColor>(e.Data);

                _pieceManager.SetColor(data);
            }

            if (e.Data.Contains(Action.INIT.ToString()))
            {
                var data = JsonUtility.FromJson<GetActionInit>(e.Data);

                _pieceManager.SetInit(data);
            }
        }

        private void Ws_InGame_OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.Data.Contains("action")) return;
            
            if (e.Data.Contains(Action.Temp.ToString()))
            {
                var data = JsonUtility.FromJson<PieceMove>(e.Data);
                
                _pieceManager.SetPieceMove(data);
            }
        }
    }
}
