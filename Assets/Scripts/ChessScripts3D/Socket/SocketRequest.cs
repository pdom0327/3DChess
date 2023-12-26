using ChessScripts3D.Managers;
using UnityEngine;
using WebSocketSharp;

namespace ChessScripts3D.Socket
{
    public class SocketRequest : SingleTon<SocketRequest>
    {
        public string url;
        private PieceManager3D _pieceManager;
        public WebSocket WsRequest { get; private set; }

        private void Awake()
        {
            _pieceManager = PieceManager3D.Instance;
            
            WsRequest = new WebSocket(url);
            
            WsRequest.OnOpen += (sender, e) => { };

            WsRequest.OnMessage += (sender, e) =>
            {
                Ws_Init_OnMessage(sender, e);
                Ws_InGame_OnMessage(sender, e);
            };
            
            WsRequest.OnClose += (sender, e) => {
                Debug.Log("[WebSocket Close (" + e.Code + ")] " + e.Reason);
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
        }
    }
}
