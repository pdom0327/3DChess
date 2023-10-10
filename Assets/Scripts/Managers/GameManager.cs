using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager s_instance;
        static GameManager Instance { get { Init(); return s_instance; } }
        
        void Start()
        {
            Init();
        }
        
        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = null;
                
                if (GameObject.Find("@GameManager") || GameObject.Find("GameManager"))
                {
                    go = new GameObject { name = "@GameManager" };
                    go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<GameManager>();
            }
        }

        private void Update()
        {
            
        }
    }
}