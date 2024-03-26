using System;
using System.Collections;
using System.Text;
using ChessScripts3D.Web.HTTPSchemas;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace ChessScripts3D.Web
{
    public class UserInfoRequest : MonoBehaviour
    {
        public IEnumerator UserInfoReq(string auth, Action<UnityWebRequest> req)
        {
            using UnityWebRequest request = Get($"{WebAPIData.Url}/user");
            
            request.SetRequestHeader("Authorization", auth);

            yield return request.SendWebRequest();

            req(request);
        }
    }
}