using System.Net.WebSockets;
using ChessScripts3D.Socket;
using ChessScripts3D.Web;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using WarpSquareEngine;
using Color = WarpSquareEngine.Color;

namespace ChessScripts3D.Managers
{
    public class GameManager : SingleTon<GameManager>
    {
        [Header("ColorDel")]
        public Color myColor;
        public delegate void ColorDelegate(GetColorAction action);
        public ColorDelegate colorDelegate;
        
        [Header("MyInfoDel")]
        public UserInfoDto myInfo;
        public delegate void MyInfoDelegate(GetColorAction action);
        public MyInfoDelegate myInfoDelegate;
        
        [Header("OpponentInfoDel")]
        public UserInfoDto opponentInfo;
        public delegate void OpponentInfoDelegate(UserInfoDto action);
        public OpponentInfoDelegate opponentInfoDelegate;

        [Header("Limit Playing")]
        public bool isReady;
        
        public Game game;
        
        
        private ChessGameWebSocket _ws;

        private CameraManager _cameraManager; 

        private void Awake()
        {
            _ws = ChessGameWebSocket.Instance;

            if (_ws != null)
            {
                colorDelegate = SetMyColor;
                opponentInfoDelegate = SetOpponentInfo;
        
                Debug.Log(myColor);
                Debug.Log(opponentInfo);
            }
            else
            {
                Debug.LogError("ChessGameWebSocket is not initialized.");
            }
        }

        private void SetMyColor(GetColorAction action)
        {
            myColor = action.color;
            
            // todo : 인스턴스 여기말고 다른곳으로 findObjects 에러 발생함
            CameraManager.Instance.setHomePos.Invoke(myColor);
        }

        private void SetOpponentInfo(UserInfoDto info) { opponentInfo = info; }
        
    }
}