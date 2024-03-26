using System;
using System.Collections;
using System.Text;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace ChessScripts3D.Web
{
    public class LoginRequest : MonoBehaviour
    {
        public IEnumerator LoginReq(LoginRequestDto loginData, Action<UnityWebRequest> req)
        {
            var jsonData = JsonUtility.ToJson(loginData, true);

            using UnityWebRequest request = Get($"{WebAPIData.Url}/auth/login");

            byte[] jsonDataBytes = new UTF8Encoding().GetBytes(jsonData);

            request.uploadHandler = new UploadHandlerRaw(jsonDataBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            
            req(request);
        }
    }
}
