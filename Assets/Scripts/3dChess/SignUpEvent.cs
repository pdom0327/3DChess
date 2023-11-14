using _3dChess.Schemas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3dChess
{
    public class SignUpEvent : MonoBehaviour
    {
        public Button goToLoginBtn;
        public Button reqSignUpBtn;
        
        public TMP_InputField userName;
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

        public SignUpRequestDto InstanceUserData()
        {
            SignUpRequestDto myData = new SignUpRequestDto
            {
                userName = userName.text,
                password = password.text,
                email = email.text
            };
            return myData;
        }
    }
}