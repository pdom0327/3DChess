using HttpRequest;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private string _roomSet;
        
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
            if (_instance == null)
            {
                GameObject go = null;
                
                if (GameObject.Find("@GameManager") || GameObject.Find("GameManager"))
                {
                    go = new GameObject { name = "@GameManager" };
                    go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<GameManager>();
            }
            
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
            
        }
    }
}