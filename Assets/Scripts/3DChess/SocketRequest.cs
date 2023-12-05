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
                Debug.Log("[WebSocket Byte] " + e.RawData);
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
