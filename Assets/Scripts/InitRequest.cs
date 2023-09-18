using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

public class InitRequest : MonoBehaviour
{
    private string _url;

    private string _roomSet;
    
    public void InitRoom()
    {
        StartCoroutine(nameof(GetRoomSet));
    }
    
    private void Start()
    {
        _url = $"https://heneinbackapi.shop/game/";
    }
    
    private IEnumerator GetRoomSet()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_url + "roomset"))
        {
            Debug.Log("Start Get connection!");
            
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success to get Data!");
                
                _roomSet = www.downloadHandler.text;

                StartCoroutine(nameof(PostInit));
            }
            else { Debug.Log($"Error! : {www.error}"); }
        }
    }

    private IEnumerator PostInit()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(_url + "init", _roomSet))
        {
            Debug.Log("Start Post connection!");
            
            www.SetRequestHeader("Room", _roomSet);
            
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success to get Data!");
                
                Debug.Log(www.downloadHandler.text);
                // 정보 처리
            }
            else { Debug.Log($"Error! : {www.error}"); }
        }
    }
}
