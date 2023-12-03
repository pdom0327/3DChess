using System.Collections;
using _3dChess;
using _3DChess;
using _3DChess.MainMenuEvents;
using _3dChess.Requests;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class MainSceneEvents : MonoBehaviour
{
    public LoginEvent logInEvents;
    public SignUpEvent signUpEvents;
    public GameMenuEvent gameMenuEvents;
    public HeaderEvent headerEvents; 

    public SignUpRequest sign;
    public LoginRequest login;
    public SocketRequest socket;

    public void Login()
    {
        StartCoroutine(RequestLogin());
    }

    public void SignUp()
    {
        StartCoroutine(RequestSignUp());
    }
    
    private IEnumerator RequestLogin()
    {
        var userData = logInEvents.InstanceLoginData();
        
        yield return StartCoroutine(login.LoginReq(userData));

        if (login.loginResult == UnityWebRequest.Result.Success)
        {
            GoToGameMenu();
        }
        else
        {
            print("UI: login error");
        }
    }
    
    private IEnumerator RequestSignUp()
    {
        var userData = signUpEvents.InstanceUserData();
        
        yield return StartCoroutine(sign.SignUpReq(userData));
        
        if (sign.signUpResult == UnityWebRequest.Result.Success)
        {
            GoToGameMenu();
        }
        else
        {
            print("UI: signup error");
        }
    }

    public void GoToLogin()
    {
        if (signUpEvents.isActiveAndEnabled)
        {
            signUpEvents.DeActivePanel();
            logInEvents.ActivePanel();    
        }
        else if (gameMenuEvents.isActiveAndEnabled)
        {
            gameMenuEvents.DeActivePanel();
            logInEvents.ActivePanel();    
        }
    }
    
    public void GoToSignUp()
    {
        logInEvents.DeActivePanel();
        signUpEvents.ActivePanel();
    }

    public void GoToGameMenu()
    {
        logInEvents.DeActivePanel();    
        gameMenuEvents.ActivePanel();
    }

    public void GameStart()
    {
        gameMenuEvents.GameStart();
    }

    public void Exit()
    {
        socket.WsRequest.Close();
        
        headerEvents.ExitGame();
    }
}