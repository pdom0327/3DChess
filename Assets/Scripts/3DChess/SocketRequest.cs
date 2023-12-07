using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

namespace _3DChess
{
    public class SocketRequest : MonoBehaviour
    {
        public string url;
        private WebSocket _wsRequest;

        
        public WebSocket WsRequest
        {
            get => _wsRequest;
            set => _wsRequest = value;
        }

        void Start()
        {
            WsRequest = new WebSocket(url);
            
            WsRequest.OnOpen += (sender, e) => { };

            WsRequest.OnMessage += (sender, e) => {
                var body = !e.IsPing ? e.Data : "A ping was received.";
                Debug.Log("[WebSocket Message] " + e.Data);


                if (!e.Data.Contains("action")) return;
                
                if (e.Data.Contains(Action.COLOR.ToString()))
                {
                    var newInit = JsonUtility.FromJson<GetActionColor>(e.Data);
                    
                    PieceManager3D.Instance.color = newInit.color;
                }
                else if (e.Data.Contains(Action.INIT.ToString()))
                {
                    var newInit = JsonUtility.FromJson<GetActionInit>(e.Data);
                    
                    PieceManager3D.Instance.InitPiece(newInit.locationList);
                }
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
       
    }
}
