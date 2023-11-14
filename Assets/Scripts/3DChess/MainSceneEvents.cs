using _3dChess;
using _3dChess.Requests;
using UnityEngine;

public class MainSceneEvents : MonoBehaviour
{
    public LoginEvent logInEvents;
    public SignUpEvent signUpEvents;

    public SignUpRequest sign;

    public void RequestLogin()
    {
        
    }
    
    public void RequestSignUp()
    {
        var userData = signUpEvents.InstanceUserData();
        StartCoroutine(sign.SignUpReq(userData));
    }

    public void GoToLogin()
    {
        signUpEvents.DeActivePanel();
        logInEvents.ActivePanel();
    }
    
    public void GoToSignUp()
    {
        logInEvents.DeActivePanel();
        signUpEvents.ActivePanel();
    }
}
