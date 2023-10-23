using System.Collections;
using Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UserRoom;

namespace HttpRequest
{
    public class InitRequest : MonoBehaviour
    {
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
            using UnityWebRequest www = UnityWebRequest.Get(_url + "roomset");
            yield return www.SendWebRequest();
                
            if (www.result == UnityWebRequest.Result.Success)
            {
                var initRoom = JsonConvert.DeserializeObject<RoomSet>(www.downloadHandler.text); 
                    
                GameManager.Instance.SetColor(initRoom.color);
                GameManager.Instance.SetRoomSet(initRoom.roomKey);

                StartCoroutine(PostInit());
            }
            else { print($"Error! : {www.error}"); }
        }
        
        private IEnumerator PostInit()
        {
            var roomSet = GameManager.Instance.GetRoomSet();

            using UnityWebRequest www = UnityWebRequest.Post(_url + "init", roomSet);
            
            www.SetRequestHeader("Room", roomSet);
                
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                PieceManager.Instance.GetJsonPieceList(www.downloadHandler.text);
                    
                yield return null;
                    
                UIManager.Instance.Fade(false);
                    
                StartCoroutine(CameraController.Instance.StartTouring(GameManager.Instance.GetColor()));

                EndInit(true);
            }
            else { print($"Error! : {www.error}"); }
        }

        public bool EndInit(bool init = false)
        {
            return init;
        }
    }
}