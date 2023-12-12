using UnityEngine;
using UnityEngine.UI;

namespace ChessScripts3D.MainMenuEvents
{
    public class HeaderEvent : MonoBehaviour
    {
        public Button exitBtn;

        void Start()
        {
            gameObject.GetComponent<RectTransform>().SetSiblingIndex(1);
        }
        
        public void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
