using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public LoadingUI loadingUI;

        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (UIManager)FindObjectOfType(typeof(UIManager));
                }

                return _instance;
            }
        }

        void Start()
        {
        }
        
        #region ClickEvent
        public void ClickEnterGame()
        {
            SceneManager.LoadScene("GameScene");
        }
        #endregion

        #region Loading
        public void Fade(bool isFadeIn)
        {
            StartCoroutine(loadingUI.StartFade(isFadeIn));
        }
        #endregion

    }
}
