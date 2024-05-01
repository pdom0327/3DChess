using System.Net.WebSockets;
using ChessScripts3D.Socket;
using ChessScripts3D.Web;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        
        public Game game = new Game();
        
        private ChessGameWebSocket _ws;
        
        [Header("Managers")]
        private CameraManager _cameraManager;
        private BoardManager3D _boardManager;
        private PieceManager3D _pieceManager;
        
        private void Awake()
        {
            _ws = ChessGameWebSocket.Instance;

            if (_ws != null)
            {
                colorDelegate = SetMyColor;
                opponentInfoDelegate = SetOpponentInfo;
            }
            else
            {
                Debug.LogError("ChessGameWebSocket is not initialized.");
            }
        }

        private void Update()
        {
            /*foreach (var piece in game.GetPiecesWithBoardType(BoardType.White))
            {
                Debug.Log(piece.GetChar() + " " + piece);
            }*/

            Debug.Log( game.GetBoards()[0].GetLevel()); // 레벨 < = >보드타입 매칭 
            game.PushBoardMove(new BoardMove(Level.White, Level.Kl3, new Option<PieceType>()));
            Debug.Log( game.GetBoards()[0].GetLevel());
            
            // todo : 피스 불러오기 

            if (_ws.currentState != GameSocketState.GameInit) return;
            SceneManager.LoadScene("3DChessGameScene");
            SceneManager.sceneLoaded -= LoadSceneInit;
            SceneManager.sceneLoaded += LoadSceneInit;
        }
        
        private void SetMyColor(GetColorAction action) { myColor = action.color; }

        private void SetOpponentInfo(UserInfoDto info) { opponentInfo = info; }

        private void LoadSceneInit(Scene scene, LoadSceneMode mode)
        {
            _cameraManager = CameraManager.Instance;
            _boardManager = BoardManager3D.Instance;
            _pieceManager = PieceManager3D.Instance;

            _cameraManager.setHomePos.Invoke(myColor);

            _ws.currentState = GameSocketState.InGamePlaying;
        }
        
    }
}