using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _3DChess.MainMenuEvents
{
    public class GameMenuEvent : MonoBehaviour
    {
        public Button gameStartBtn;
        public Button logOutBtn;
    
        public void ActivePanel()
        {
            gameObject.SetActive(true);
        }

        public void DeActivePanel()
        {
            gameObject.SetActive(false);
        }

        public void GameStart()
        {
            SceneManager.LoadScene("3DChessGameScene");
        }
    }
}
