using System.Collections;
using System.Collections.Generic;
using Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using RoomSet;

namespace HttpRequest
{
    public class InitRequest : MonoBehaviour
    {
        public string color;

        private string _url;
        
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
        }

        public IEnumerator InitRoom()
        {
            yield return StartCoroutine(GetRoomSet());
        }
        
        private IEnumerator GetRoomSet()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(_url + "roomset"))
            {
                yield return www.SendWebRequest();
                
                if (www.result == UnityWebRequest.Result.Success)
                {
                    var initRoom = JsonConvert.DeserializeObject<RoomSet.RoomSet>(www.downloadHandler.text); 
                    
                    color = initRoom.color;
                    GameManager.Instance.SetRoomSet(initRoom.roomKey);

                    StartCoroutine(PostInit());
                }
                else { Debug.Log($"Error! : {www.error}"); }
            }
        }

        private IEnumerator PostInit()
        {
            var roomSet = GameManager.Instance.GetRoomSet();
            
            using (UnityWebRequest www = UnityWebRequest.Post(_url + "init", roomSet))
            {
                www.SetRequestHeader("Room", roomSet);
                
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    PieceManager.Instance.GetJsonPieceList(www.downloadHandler.text);
                    
                    yield return null;
                    
                    UIManager.Instance.Fade(false);
                    
                    StartCoroutine(CameraController.Instance.StartTouring(color));

                    EndInit(true);
                }
                else { Debug.Log($"Error! : {www.error}"); }
            }
        }

        public bool EndInit(bool init = false)
        {
            return init;
        }
    }
}