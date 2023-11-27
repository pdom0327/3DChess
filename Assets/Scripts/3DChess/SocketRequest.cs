using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace _3DChess
{
    public class SocketRequest : MonoBehaviour
    {
        public string url;

        void Start()
        {
            var ws = new WebSocket(url);
            //ws.SetProxy("http://3dchess.shop/game/start", "", "");

            Debug.Log(ws.Log.Level);
            
            ws.OnOpen += (sender, e) => ws.Send ("Hi, there!");

            ws.OnMessage += (sender, e) => {
                var body = !e.IsPing ? e.Data : "A ping was received.";
                Debug.Log("[WebSocket Message] " + body);
            };

            ws.OnError += (sender, e) => {
                Debug.LogError("[WebSocket Error] " + e.Message);
            };

            ws.OnClose += (sender, e) => {
                Debug.Log("[WebSocket Close (" + e.Code + ")] " + e.Reason);
                Debug.Log(e.Reason + "?");
            };
            
            ws.OnError += (sender, e) => {
                Debug.LogError("[WebSocket Error] " + e.Message);
                if (e.Exception != null) {
                    Debug.LogError("Exception: " + e.Exception.ToString());
                }
            };
            
            ws.Connect ();
            
            Debug.Log("=============");
            Debug.Log(ws.Log.Level);
        }
    }
}
