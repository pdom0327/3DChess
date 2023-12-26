using UnityEngine;
using UnityEngine.UI;

namespace ChessScripts3D.MainMenuEvents
{
    public class HeaderEvent : MonoBehaviour
    {
        public Button exitBtn;

        private void Awake()
        {
            gameObject.SetActive(true);
        }
        
        void Start()
        {
            gameObject.GetComponent<RectTransform>().SetSiblingIndex(10);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
