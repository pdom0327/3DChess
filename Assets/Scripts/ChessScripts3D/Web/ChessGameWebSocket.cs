using ChessScripts3D.Managers;
using ChessScripts3D.Socket;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using WebSocketSharp;

namespace ChessScripts3D.Web
{
    public enum GameSocketState
    {
        Matching,
        Matched,
        GameInit,
        InGamePlaying,
    }
    
    public class ChessGameWebSocket : SingleTon<ChessGameWebSocket>
    {
        public WebSocket socket;

        public GameSocketState currentState;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            Debug.Log(_gameManager);
            
            socket.OnOpen += (sender, e) => { };
            
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
            currentState = GameSocketState.Matching;
            
            var auth = WebRequests.Instance.GetAuthorization();

            socket = new WebSocket(WebAPIData.SocketUrl + $"/game/start?token={auth}");

            socket.Connect();
        }

        public void OnDisable()
        {
            socket.Close();
        }

        private void SetUpAction(MessageEventArgs e)
        {
            if (e.Data.Contains(SocketAction.COLOR.ToString()))
            {
                var actionObject = JsonUtility.FromJson<GetColorAction>(e.Data);
                _gameManager.colorDelegate.Invoke(actionObject);
            }
            else if (e.Data.Contains(SocketAction.MATCHED_USER.ToString()))
            {
                var actionObject = JsonUtility.FromJson<UserInfoDto>(e.Data);
                _gameManager.opponentInfoDelegate.Invoke(actionObject);
            }
            else if (e.Data.Contains(SocketAction.INIT.ToString()))
            {
                var actionObject = JsonUtility.FromJson<GetInitAction>(e.Data);
                
                currentState = GameSocketState.Matched;
            }
        }
        
    }
}