using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class InitRequest : MonoBehaviour
{
    private string _url;

    private string _roomSet;

    private static InitRequest _instance;

    public static InitRequest Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = (InitRequest)FindObjectOfType(typeof(InitRequest));
            }
            return _instance;
        }
    }

    private void Start()
    {
        _url = $"https://heneinbackapi.shop/game/";

        StartCoroutine(nameof(InitRoom));
    }
    
    public IEnumerator InitRoom()
    {
        yield return StartCoroutine(nameof(GetRoomSet));
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

                List<Piece.Piece> pieces = JsonConvert.DeserializeObject<List<Piece.Piece>>(www.downloadHandler.text);
                
                // 정보 처리
                
                foreach (var piece in pieces)
                {
                    Debug.Log($"ID: {piece.id}, X: {piece.x}, Y: {piece.y}, HasMoved: {piece.hasMoved}");
                }
                
                yield return null;
                
                UIManager.Instance.Fade(false);
            }
            else { Debug.Log($"Error! : {www.error}"); }
        }
    }
}
