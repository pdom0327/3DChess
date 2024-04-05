using System;
using System.Collections;
using System.Text;
using ChessScripts3D.Web.HTTPSchemas;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace ChessScripts3D.Web
{
    public class SignUpRequest : MonoBehaviour
    {
        public IEnumerator SignUpReq(SignUpRequestDto signUpData, Action<UnityWebRequest> req)
        {
            var jsonData = JsonUtility.ToJson(signUpData);
            
            using UnityWebRequest request = PostWwwForm($"{WebAPIData.Url}/auth/sign-up", string.Empty);

            byte[] jsonDataBytes = new UTF8Encoding().GetBytes(jsonData);
                
            request.uploadHandler = new UploadHandlerRaw(jsonDataBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();
            
            req(request);
        }
    }
}