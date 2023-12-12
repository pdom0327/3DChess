using ChessScripts3D.HTTPSchemas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChessScripts3D.MainMenuEvents
{
    public class LoginEvent : MonoBehaviour
    {
        public Button goToSignUpBtn;
        public Button reqLoginBtn;

        public TMP_InputField email;
        public TMP_InputField password;
        
        public void ActivePanel()
        {
            gameObject.SetActive(true);
        }
        
        public void DeActivePanel()
        {
            gameObject.SetActive(false);
        }

        public LoginRequestDto InstanceLoginData()
        {
            LoginRequestDto myData = new LoginRequestDto()
            {
                email = email.text,
                password = password.text
            };
            return myData;
        }
    }
}