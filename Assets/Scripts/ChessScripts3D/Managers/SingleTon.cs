using UnityEngine;

namespace ChessScripts3D.Managers
{
    public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
    }
}
