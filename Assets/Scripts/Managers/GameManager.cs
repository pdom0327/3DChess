using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager s_instance;
        static GameManager Instance { get { Init(); return s_instance; } }
        
        void Start()
        {
        
        }
        
        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@GameManager") ;
                if (!go)
                {
                    go = new GameObject { name = "@GameManager" };
                    go.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<GameManager>();
            }
        }
    }
}