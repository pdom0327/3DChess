using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
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

        public void ClickEnterGame()
        {
            StartCoroutine(nameof(InitProgress));
        } 
        
        public IEnumerator InitProgress()
        {
            yield return InitRequest.Instance.InitRoom();

            SceneManager.LoadScene("GameScene");
        }
    }
}
