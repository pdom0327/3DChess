using System;
using System.Diagnostics.Tracing;
using ChessScripts3D.Managers;
using ChessScripts3D.Socket;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace ChessScripts3D.Web
{
    public class ChessGameWebSocket : SingleTon<ChessGameWebSocket>
    {
        public bool isMatching;
        
        public WebSocket socket;
        
        void Update()
        {
            if ( !isMatching ) return ;
            
            var auth = WebRequests.Instance.GetAuthorization();

            socket = new WebSocket(WebAPIData.SocketUrl + $"/game/start?token={auth}");
            
            socket.OnOpen += (sender, e) => { Debug.Log("hello"); };

            socket.OnMessage += (sender, e) =>
            {
                SetUpAction(e);
            };
            
            socket.OnClose += (sender, e) => {
                if (e.Code == 1000)
                {
                    StopMatching();
                }
            };
            
            socket.OnError += (sender, e) => {
                if (e.Exception != null) {
                    Debug.LogError("Exception: " + e.Exception);
                }
            };
                
            socket.Connect();

            EndMatching();
        }

        public void StartMatching() { isMatching = WebAPIData.matchStart; }

        private void EndMatching() { isMatching = WebAPIData.matchEnd; }

        public void StopMatching()
        {
            EndMatching();
            socket.Close();
        }

        public void SetUpAction(MessageEventArgs e)
        {
            var gameManager = GameManager.Instance;

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