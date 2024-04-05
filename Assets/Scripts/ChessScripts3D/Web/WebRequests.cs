using System;
using ChessScripts3D.Managers;
using ChessScripts3D.Web;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;


namespace ChessScripts3D.Web
{
    [RequireComponent(typeof(LoginRequest))]
    [RequireComponent(typeof(SignUpRequest))]
    [RequireComponent(typeof(RefreshRequest))]
    [RequireComponent(typeof(UserInfoRequest))]
    [RequireComponent(typeof(ErrorCollection))]
    public class WebRequests : SingleTon<WebRequests>
    {
        [SerializeField] public LoginRequest login;
        [SerializeField] public SignUpRequest sign;
        [SerializeField] public RefreshRequest refresh;
        [SerializeField] public UserInfoRequest userInfo;
        [SerializeField] public ErrorCollection errorCollection;
        
        private string _authorization;
        private string _refreshToken;

        private void Awake()
        {
            login = login == null ? gameObject.AddComponent<LoginRequest>() : login;
            sign = sign == null ? gameObject.AddComponent<SignUpRequest>() : sign;
            userInfo = userInfo == null ? gameObject.AddComponent<UserInfoRequest>() : userInfo;
            refresh = refresh == null ? gameObject.AddComponent<RefreshRequest>() : refresh;
            errorCollection = errorCollection == null ? gameObject.AddComponent<ErrorCollection>() : errorCollection;
            
            login = GetComponent<LoginRequest>();
            sign = GetComponent<SignUpRequest>();
            refresh = GetComponent<RefreshRequest>();
            userInfo = GetComponent<UserInfoRequest>();
            errorCollection = GetComponent<ErrorCollection>();
        }
        
        public string GetAuthorization()
        {
            var auth = _authorization;
            return auth;
        }

        public string GetRefreshToken()
        {
            var token = _refreshToken;
            return token;
        }

        public void SetAuthorization(string auth)
        {
            _authorization = auth;
        }

        public void SetRefreshToken(string token)
        {
            _refreshToken = token;
        }

        public ButtonNeedFunction ErrorControl(ErrorBox errorBox, Action callback)
        {
            var error = errorCollection.CatchError(errorBox);

            switch (error)
            {
                case WebError.Unauthorized:
                    print("로그인 회원가입 인증에러");
                    return ButtonNeedFunction.Back;
                
                case WebError.Forbidden:
                    print("클라인언트 권한 문제");
                    return ButtonNeedFunction.Back;
                    
                case WebError.NotFound:
                    print("Url문제 해당 페이지를 찾지 못함");
                    return ButtonNeedFunction.Back;
                
                case WebError.Refresh:
                    print("리프레시 토큰 만료");
                    StartCoroutine(refresh.RefreshReq(_refreshToken, req =>
                    {
                        var auth = req.GetResponseHeader(WebAPIData.GetAuthKey());
                        SetAuthorization(auth);
                        
                        callback();
                    }));
                    return ButtonNeedFunction.Nothing;
                
                case WebError.SecurityTampering:
                    print("ErrorControl : 보안 조작 에러");
                    return ButtonNeedFunction.Quit;

                default :
                    return ButtonNeedFunction.Nothing;
            }
        }
    }
}