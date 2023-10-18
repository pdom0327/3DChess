using System.Collections;
using System.Collections.Generic;
using Managers;
using Newtonsoft.Json;
using Pieces;
using UnityEngine;
using UnityEngine.Networking;

namespace HttpRequest
{
    public class ClickRequest : MonoBehaviour
    {
        private string _url;

        private Piece _clickedPiece;

        private static ClickRequest _instance;

        public static ClickRequest Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (ClickRequest)FindObjectOfType(typeof(ClickRequest));
                }
                return _instance;
            }
        }
        
        void Start()
        {
            _url = $"https://heneinbackapi.shop/game/piece/valid-list";
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator GetPoint(int clickedPieceId)
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_url}/{clickedPieceId}");
            
            www.SetRequestHeader("Room", GameManager.Instance.GetRoomSet());
                
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
                var points = JsonConvert.DeserializeObject<List<Point>>(www.downloadHandler.text);

                BoardManager.Instance.ActiveCell(points);
            }
            else
            {
                Debug.Log($"Error! : {www.error}");
            }
        }
    }    
}