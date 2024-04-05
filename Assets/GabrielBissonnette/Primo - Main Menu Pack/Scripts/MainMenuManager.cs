using System;
using System.Collections;
using System.Collections.Generic;
using ChessScripts3D.Web;
using ChessScripts3D.Web.HTTPSchemas;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WebSocketSharp;
using static UnityEngine.Networking.UnityWebRequest;


public class MainMenuManager : MonoBehaviour
{
    #region Variables

    [Header("On/Off")]
    [Space(5)] [SerializeField] bool showBackground;
    [SerializeField] bool showSocial1;
    [SerializeField] bool showSocial2;
    [SerializeField] bool showSocial3;
    [SerializeField] bool showVersion;
    [SerializeField] bool showFade;

    [Header("Scene")]
    [Space(10)] [SerializeField] string playSceneToLoad;
    [SerializeField] string sandBoxSceneToLoad;

    [Header("Sprites")]
    [Space(10)] [SerializeField] Sprite logo;
    [SerializeField] Sprite background;
    [SerializeField] Sprite buttons;
    
    [Header("Color")]
    [Space(10)] [SerializeField] Color32 mainColor;
    [SerializeField] Color32 secondaryColor;

    [Header("Version")]
    [Space(10)] [SerializeField] string version = "v.0105";

    [Header("Texts")]
    [Space(10)] [SerializeField] string play = "Play";
    [SerializeField] string settings = "Settings";
    [SerializeField] string quit = "Quit";

    [Header("Social")]
    [Space(10)] [SerializeField] Sprite social1Icon;
    [SerializeField] string social1Link;
    [Space(5)]
    [SerializeField] Sprite social2Icon;
    [SerializeField] string social2Link;
    [Space(5)]
    [SerializeField] Sprite social3Icon;
    [SerializeField] string social3Link;
    List<string> links = new List<string>();

    [Header("Audio")]
    [Space(10)] [SerializeField] float defaultVolume = 0.8f;
    [SerializeField] AudioClip uiClick;
    [SerializeField] AudioClip uiHover;
    [SerializeField] AudioClip uiSpecial;


    // Components
    [Header("Components")]
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject signUpPanel;
    [SerializeField] GameObject homePanel;
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject profilePanel;
    [SerializeField] GameObject matchingPanel;
    [SerializeField] GameObject alertPanel;
    [SerializeField] Image social1Image;
    [SerializeField] Image social2Image;
    [SerializeField] Image social3Image;
    [SerializeField] Image logoImage;
    [SerializeField] Image backgroundImage;

    [Header("Fade")]
    [Space(10)] [SerializeField] Animator fadeAnimator;

    [Header("Color Elements")]
    [Space(5)] [SerializeField] Image[] mainColorImages;
    [SerializeField] TextMeshProUGUI[] mainColorTexts;
    [SerializeField] Image[] secondaryColorImages;
    [SerializeField] TextMeshProUGUI[] secondaryColorTexts;
    [SerializeField] Image[] buttonsElements;

    [FormerlySerializedAs("loginText")]
    [Header("Texts")]
    [Space(10)] [SerializeField] TextMeshProUGUI playText;
    [SerializeField] TextMeshProUGUI settingsText;
    [SerializeField] TextMeshProUGUI quitText;
    [SerializeField] TextMeshProUGUI versionText;

    [Header("UserProfileText")] 
    [Space(5)] 
    [SerializeField] TextMeshProUGUI profileUserNameText;
    [SerializeField] TextMeshProUGUI profileTierText;
    [SerializeField] TextMeshProUGUI profileScoreText;
    
    [Header("Settings")]
    [Space(10)] [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("System")] 
    [SerializeField] WebRequests webRequest;
    [SerializeField] ChessGameWebSocket webSocket;

    [Header("matchingTimeText")]
    [SerializeField] TextMeshProUGUI matchingTimeText;
    
    [Header("Button Interactable")] 
    [SerializeField] List<Button> controlButtons = new List<Button>();

    [Header("Login")] 
    [SerializeField] TMP_InputField loginEmailInput;
    [SerializeField] TMP_InputField loginPasswordInput;

    [Header("SignUp")] 
    [SerializeField] TMP_InputField signUpEmailInput;
    [SerializeField] TMP_InputField signUpUserNameInput;
    [SerializeField] TMP_InputField signUpPasswordInput;
    
    [Header("Alert")] 
    [SerializeField] TextMeshProUGUI alertText;
    [SerializeField] Button alertBackButton;
    [SerializeField] Button alertQuitButton;
    
    Resolution[] resolutions;
    private Coroutine _nowMatching;
    #endregion

    void Start()
    {
        SetStartUI();
        ProcessLinks();
        SetStartVolume();
        //PrepareResolutions();
    }

    private void SetStartUI()
    {
        //fadeAnimator.SetTrigger("FadeIn");
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        profilePanel.SetActive(false);
        matchingPanel.SetActive(false);
    }

    public void SetHomeUI()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
        homePanel.SetActive(true);
        playPanel.SetActive(false);
        profilePanel.SetActive(false);
        matchingPanel.SetActive(false);
    }

    public void SetPlayUI()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(true);
        profilePanel.SetActive(false);
        matchingPanel.SetActive(false);
    }
    
    public void SetProfileUI()
    {
        homePanel.SetActive(false);
        playPanel.SetActive(false);
        profilePanel.SetActive(true);
    }

    public void SetMatchingUI()
    {
        playPanel.SetActive(false);
        matchingPanel.SetActive(true);   
    }

    public void UIEditorUpdate()
    {
        // Used to update the UI when not in play mode

        #region Sprites

        // Logo
        if (logoImage is not null)
        {
            logoImage.sprite = logo;
            logoImage.color = mainColor;
            logoImage.SetNativeSize();
        }

        // Background
        if (backgroundImage is not null)
        {
            backgroundImage.gameObject.SetActive(showBackground);
            backgroundImage.sprite = background;
            backgroundImage.SetNativeSize();
        }

        // Main Color Images
        for (int i = 0; i < mainColorImages.Length; i++)
        {
            mainColorImages[i].color = mainColor;
        }

        // Main Color Texts
        for (int i = 0; i < mainColorTexts.Length; i++)
        {
            mainColorTexts[i].color = mainColor;
        }

        // Secondary Color Images
        for (int i = 0; i < secondaryColorImages.Length; i++)
        {
            secondaryColorImages[i].color = secondaryColor;
        }

        // Secondary Color Texts
        for (int i = 0; i < secondaryColorTexts.Length; i++)
        {
            secondaryColorTexts[i].color = secondaryColor;
        }

        // Buttons Elements
        for (int i = 0; i < buttonsElements.Length; i++)
        {
            buttonsElements[i].sprite = buttons;
        }

        // Fade
        fadeAnimator.gameObject.SetActive(showFade);

        #endregion


        #region Texts

        if (playText != null)
            playText.text = play;

        if (settingsText != null)
            settingsText.text = settings;

        if (quitText != null)
            quitText.text = quit;

        // Version number
        versionText.gameObject.SetActive(showVersion);
        if (versionText != null)
            versionText.text = version;

        #endregion


        #region Social

        if (social1Image != null)
        {
            social1Image.sprite = social1Icon;
            social1Image.gameObject.SetActive(showSocial1);
        }

        if (social2Image != null)
        {
            social2Image.sprite = social2Icon;
            social2Image.gameObject.SetActive(showSocial2);
        }

        if (social3Image != null)
        {
            social3Image.sprite = social3Icon;
            social3Image.gameObject.SetActive(showSocial3);
        }

        #endregion
    }

    #region Links
    public void OpenLink(int _index)
    {
        if(links[_index].Length > 0)
            Application.OpenURL(links[_index]);
    }

    private void ProcessLinks()
    {
        if (social1Link.Length > 0)
            links.Add(social1Link);

        if (social2Link.Length > 0)
            links.Add(social2Link);

        if (social3Link.Length > 0)
            links.Add(social3Link);
    }
    #endregion


    #region Levels
    public void LoadLevel()
    {
        // Fade Animation
        fadeAnimator.SetTrigger("FadeOut");

        StartCoroutine(WaitToLoadLevel());
    }

    IEnumerator WaitToLoadLevel()
    {
        yield return new WaitForSeconds(1f);

        // Scene Load
        SceneManager.LoadScene(playSceneToLoad);
    }
    
    IEnumerator WaitToLoadSandBox()
    {
        yield return new WaitForSeconds(1f);

        // Scene Load
        SceneManager.LoadScene(playSceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion


    #region Audio

    public void SetVolume(float _volume)
    {
        // Adjust volume
        AudioListener.volume = _volume;

        // Save volume
        PlayerPrefs.SetFloat("Volume", _volume);
    }

    void SetStartVolume()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", defaultVolume);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void UIClick()
    {
        audioSource.PlayOneShot(uiClick);
    }

    public void UIHover()
    {
        audioSource.PlayOneShot(uiHover);
    }

    public void UISpecial()
    {
        audioSource.PlayOneShot(uiSpecial);
    }

    #endregion


    #region Graphics & Resolution Settings

    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void PrepareResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if(!options.Contains(option))
                options.Add(option);

            if(i == resolutions.Length - 1)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion

    #region WebSystem

    private void DisableButton()
    {
        foreach (var button in controlButtons) { button.interactable = false; }
    }

    private void EnableButton()
    {
        foreach (var button in controlButtons) { button.interactable = true; }
    }
    
    public void TryLogin()
    {
        DisableButton();
        
        var loginData = new LoginRequestDto() { 
            email = loginEmailInput.text, 
            password = loginPasswordInput.text
        };
        
        StartCoroutine(webRequest.login.LoginReq(loginData, req =>
        {
            if (req.result == Result.Success)
            {
                var auth = req.GetResponseHeader(WebAPIData.GetAuthKey());
                var token = req.GetResponseHeader(WebAPIData.GetRefreshKey());
                webRequest.SetAuthorization(auth);
                webRequest.SetRefreshToken(token);
                
                SetHomeUI();
            }
            else
            {
                var errorBox = JsonUtility.FromJson<ErrorBox>(req.downloadHandler.text);

                var buttonAction = webRequest.ErrorControl(errorBox, TryLogin);
                Alert(buttonAction);
                alertText.text = errorBox.errorMessage;
            }
        }));
        
        EnableButton();
    }
    
    public void TrySignUp()
    {
        DisableButton();
        
        var signUpData = new SignUpRequestDto() {
            userName = signUpUserNameInput.text, 
            password = signUpPasswordInput.text,
            email = signUpEmailInput.text
        };

        StartCoroutine(webRequest.sign.SignUpReq(signUpData, req =>
        {
            if (req.result == Result.Success)
            {
                var auth = req.GetResponseHeader(WebAPIData.GetAuthKey());
                var token = req.GetResponseHeader(WebAPIData.GetRefreshKey());
                webRequest.SetAuthorization(auth);
                webRequest.SetRefreshToken(token);
                
                SetHomeUI();
            }
            else
            {
                var errorBox = JsonUtility.FromJson<ErrorBox>(req.downloadHandler.text);
                webRequest.ErrorControl(errorBox, TrySignUp);

                var buttonAction = webRequest.ErrorControl(errorBox, TryLogin);
                Alert(buttonAction);
                alertText.text = errorBox.errorMessage;
            }
        }));
        
        EnableButton();
    }
    
    public void TryGetUserInfo()
    {
        DisableButton();
        
        StartCoroutine(webRequest.userInfo.UserInfoReq(webRequest.GetAuthorization(),req =>
        {
            if (req.result == Result.Success)
            {
                var userData = JsonUtility.FromJson<UserInfoDto>(req.downloadHandler.text);

                profileUserNameText.text = userData.userName;
                profileTierText.text = $"{userData.tier}";
                profileScoreText.text = $"{userData.score}";

                SetProfileUI();
            }
            else
            {
                var errorBox = JsonUtility.FromJson<ErrorBox>(req.downloadHandler.text);
                webRequest.ErrorControl(errorBox, TryGetUserInfo);

                var buttonAction = webRequest.ErrorControl(errorBox, TryLogin);
                Alert(buttonAction);
                alertText.text = errorBox.errorMessage;
            }
        }));
        
        EnableButton();
    }

    public void TryMatching()
    {
        DisableButton();
        
        SetMatchingUI();
        
        webSocket.StartMatching();
        
        _nowMatching = StartCoroutine(MatchingTimer());
        
        EnableButton();
    }

    public void StopMatching()
    {
        StopCoroutine(_nowMatching);
        
        webSocket.StopMatching();
        
        SetPlayUI();
    }

    private void Alert(ButtonNeedFunction buttonAction)
    {
        alertPanel.SetActive(true);
        
        switch (buttonAction)
        {
            case ButtonNeedFunction.Back:
                alertQuitButton.GameObject().SetActive(false);
                alertBackButton.GameObject().SetActive(true);
                break;
            case ButtonNeedFunction.Quit:
                alertBackButton.GameObject().SetActive(false);
                alertQuitButton.GameObject().SetActive(true);
                break;
            case ButtonNeedFunction.Nothing: default:
                return;
        }
    }

    private IEnumerator MatchingTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 40)
        {
            elapsedTime += Time.deltaTime;
            
            matchingTimeText.text = $"0:{Mathf.FloorToInt(elapsedTime).ToString()}";

            yield return null;
        }

        matchingTimeText.text = "matching fail";
    }
    #endregion
}
