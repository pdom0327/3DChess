using System.Collections;
using System.Collections.Generic;
using Managers;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace HttpRequest
{
    public class InitRequest : MonoBehaviour
    {
        public string color;
        
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

            StartCoroutine(InitRoom());
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
                    RoomSet.RoomSet initRoom = JsonConvert.DeserializeObject<RoomSet.RoomSet>(www.downloadHandler.text);
                    
                    color = initRoom.color;
                    _roomSet = initRoom.roomKey;

                    StartCoroutine(PostInit());
                }
                else { Debug.Log($"Error! : {www.error}"); }
            }
        }

        private IEnumerator PostInit()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(_url + "init", _roomSet))
            {
                www.SetRequestHeader("Room", _roomSet);
                
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    List<Piece.Piece> pieces = JsonConvert.DeserializeObject<List<Piece.Piece>>(www.downloadHandler.text);

                    foreach (var piece in pieces)
                    {
                        PieceManager.Instance.InitPieceById(piece.id, piece.x, piece.y);
                    }
                    
                    yield return null;
                    
                    UIManager.Instance.Fade(false);
                }
                else { Debug.Log($"Error! : {www.error}"); }
            }

            StartCoroutine(CameraController.Instance.StartTouring(color));

            EndInit(true);
        }

        public bool EndInit(bool init)
        {
            return init;
        }
    }
}