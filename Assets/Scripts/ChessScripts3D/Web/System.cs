using System;
using UnityEngine;

namespace ChessScripts3D.Web
{
    public class System : MonoBehaviour
    {
        private void Awake()
        {
            var obj = FindObjectsOfType<System>();
            
            if (obj.Length == 1)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}