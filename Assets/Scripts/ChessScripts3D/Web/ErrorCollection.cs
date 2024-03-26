using System.Collections.Generic;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using static ChessScripts3D.Web.WebError;

namespace ChessScripts3D.Web
{
    public enum WebError
    {
        Unauthorized,
        Forbidden,
        NotFound,
        Refresh,
        SecurityTampering,
        something
    }

    public enum ButtonNeedFunction
    {
        Nothing,
        Back,
        Quit
    }

    public class ErrorCollection : MonoBehaviour
    {
        public WebError CatchError(ErrorBox errorBox)
        {
            var code = errorBox.code;
            
            switch (code)
            {
                // Login SignUp has wrong access
                case 401:
                    return Unauthorized;
                // Client don't have authority about request
                case 403:
                    return Forbidden;
                // Url has a problem
                case 404:
                    return NotFound;
                // Refresh Token Error
                case 101: case 4006:
                    return Refresh;
                // Security tampering
                case 103: case 104: case 105: 
                    return SecurityTampering;
                default:
                    print("내가 모르는 에러");
                    return something;
            }
            
        }
    }
}