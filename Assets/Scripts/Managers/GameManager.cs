using DefaultNamespace;
using HttpRequest;
using Pieces;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private string _roomSet;

        private Piece _clickedPiece;
        
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                }
                return _instance;
            }
        }

        void Start()
        {
            /*if (_instance == null)
            {
                GameObject go = null;
                
                if (GameObject.Find("@GameManager") || GameObject.Find("GameManager"))
                {
                    go = new GameObject { name = "@GameManager" };
                    go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<GameManager>();
            }*/
            
            StartCoroutine(InitRequest.Instance.InitRoom());
        }

        public string GetRoomSet()
        {
            return _roomSet;
        }
        
        public void SetRoomSet(string roomSet)
        {
            _roomSet = roomSet;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return ;
            ClickEvent.Instance.ClickPiece();
        }
    }
}