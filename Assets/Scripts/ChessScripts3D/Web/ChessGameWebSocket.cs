using System;
using System.Diagnostics.Tracing;
using ChessScripts3D.Managers;
using ChessScripts3D.Socket;
using ChessScripts3D.Web.HTTPSchemas;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace ChessScripts3D.Web
{
    public class ChessGameWebSocket : SingleTon<ChessGameWebSocket>
    {
        public WebSocket socket;

        private void Start()
        {
            socket.OnOpen += (sender, e) => { Debug.Log("hello"); };

            socket.OnMessage += (sender, e) =>
            {
                Debug.Log(e.Data);
                SetUpAction(e);
            };
            
            socket.OnClose += (sender, e) => {
                if (e.Code == 1000)
                {
                    socket.Close();
                }
            };
            
            socket.OnError += (sender, e) => {
                if (e.Exception != null) {
                    Debug.LogError("Exception: " + e.Exception);
                }
            };
        }

        public void OnEnable()
        {
            var auth = WebRequests.Instance.GetAuthorization();

            socket = new WebSocket(WebAPIData.SocketUrl + $"/game/start?token={auth}");

            socket.Connect();
        }

        public void OnDisable()
        {
            socket.Close();
        }

        public void SetUpAction(MessageEventArgs e)
        {
            if (e.Data.Contains(SocketAction.COLOR.ToString()))
            {
                var actionObject = JsonUtility.FromJson<GetColorAction>(e.Data);
            }
            else if (e.Data.Contains(SocketAction.MATCHED_USER.ToString()))
            {
                var actionObject = JsonUtility.FromJson<UserInfoDto>(e.Data);
            }
            else if (e.Data.Contains(SocketAction.INIT.ToString()))
            {
                var actionObject = JsonUtility.FromJson<GetInitAction>(e.Data);
            }
                
        }
        
    }
}