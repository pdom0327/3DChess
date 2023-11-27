using System.Collections;
using System.Collections.Generic;
using System.Text;
using _3dChess.Schemas;
using UnityEngine;
using UnityEngine.Networking;

public class LoginRequest : MonoBehaviour
{
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

    public IEnumerator LoginReq(LoginRequestDto loginData)
    {
        var jsonData = JsonUtility.ToJson(loginData);
        
        using UnityWebRequest request = UnityWebRequest.Get($"{URL}/login");

        byte[] jsonDataBytes = new UTF8Encoding().GetBytes(jsonData);

        request.uploadHandler = new UploadHandlerRaw(jsonDataBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        
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
