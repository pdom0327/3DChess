using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomRequest : MonoBehaviour
{
    private string _url;

    public string URL
    {
        get => _url;
        set => _url = value;
    }

    void Start()
    {
        URL = "https://3dchess.shop/auth";

        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        using UnityWebRequest request = UnityWebRequest.Get($"{URL}/header/test");
        
        request.SetRequestHeader("Authorization", "Anything To String");
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            print(request.downloadHandler.text);
        }
        else
        {
            print($"Error! : {request.error}");
        }
    }
}
