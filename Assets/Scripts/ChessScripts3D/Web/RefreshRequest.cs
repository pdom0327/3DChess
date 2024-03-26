using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace ChessScripts3D.Web
{
    public class RefreshRequest : MonoBehaviour
    {
        public IEnumerator RefreshReq(string refreshToken, Action<UnityWebRequest> req)
        {
            using UnityWebRequest request = PostWwwForm($"{WebAPIData.Url}/auth/refresh", string.Empty);
        
            request.SetRequestHeader(WebAPIData.GetRefreshKey(), refreshToken);

            yield return request.SendWebRequest();
            
            req(request);
        }
    }
}
