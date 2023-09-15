using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour
{
    private string _url;

    private string _roomSet;

    private string _init;
    
    private static Network _instance = null;

    public static Network Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Network)FindObjectOfType(typeof(Network));
            }
            return _instance;
        }
    }
    void Start()
    {
        _url = $"https://heneinbackapi.shop/game/roomset";

        StartCoroutine(GetRoomSet());
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        
        www.SetRequestHeader("Room", _roomSet);
        
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.data);
            //www.Dispose();
        }
        else
        {
            Debug.Log("error");
            //www.Dispose();
        }
    }

    public IEnumerator GetRoomSet()
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);

        yield return www.SendWebRequest();
        
        Debug.Log("Aa");
        
        if (www.error == null)
        {
            _roomSet = www.downloadHandler.text;
            Debug.Log(_roomSet);
            www.Dispose();
        }
        else
        {
            Debug.Log("a");
            www.Dispose();
        }
    }

    /*IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("파라메타", "데이터");
 
        UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);
        www.SetRequestHeader("헤더", "헤더 값");
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("성공!");
        }
    }*/
}
