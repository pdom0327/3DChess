using System.Collections;
using System.Text;
using _3dChess.Schemas;
using UnityEngine;
using UnityEngine.Networking;

namespace ChessScripts3D.Requests
{
    public class SignUpRequest : MonoBehaviour
    {
        public UnityWebRequest.Result signUpResult; 
        
        private string _url;

        public string URL
        {
            get => _url;
            set => _url = value;
        }

        private void Start()
        {
            URL = "https://3dchess.shop/auth";
        }
        
        public IEnumerator SignUpReq(SignUpRequestDto signUpData)
        {
            var jsonData = JsonUtility.ToJson(signUpData);
            
            using UnityWebRequest request = UnityWebRequest.PostWwwForm($"{URL}/sign-up", string.Empty);

            byte[] jsonDataBytes = new UTF8Encoding().GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(jsonDataBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            signUpResult = request.result;
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                print($"Success! : {request.downloadHandler.text}");
            }
            else
            {
                print($"Error! : {request.error}");
            }

        }
    }
}