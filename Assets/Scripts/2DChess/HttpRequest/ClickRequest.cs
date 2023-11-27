using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Boards;
using Managers;
using UnityEngine;
using UnityEngine.Networking;

namespace HttpRequest
{
    public class ClickRequest : MonoBehaviour
    {
        private Uri _url;

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
            _url = new Uri($"https://heneinbackapi.shop/game/piece");
        }
        
        public IEnumerator GetPoint(int clickedPieceId)
        {
            using UnityWebRequest www = UnityWebRequest.Get($"{_url}/valid-list/{clickedPieceId}");
            
            www.SetRequestHeader("Room", GameManager.Instance.GetRoomSet());
                
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var points = JsonUtility.FromJson<List<Point>>(www.downloadHandler.text);
                
                if (points != null)
                    BoardManager.Instance.ActiveCell(points);
            }
            else
            {
                print($"Error! : {www.error}");
            }
        }
        // https://forum.unity.com/threads/unity-2021-unitywebrequest-native-collection-memory-leak.1175612/
        public IEnumerator PostMovePiece(Collider col)
        {
            var cell = col.GetComponent<BoardCell>();
            
            string jsonData = JsonUtility.ToJson(new
            {
                hasMoved = true,
                GameManager.Instance.clickedPiece.pieceData.id,
                cell.cellPosition.x,
                cell.cellPosition.y
            });

            using UnityWebRequest www = UnityWebRequest.PostWwwForm($"{_url}/move",  string.Empty);
            
            byte[] jsonDataBytes = new UTF8Encoding().GetBytes(jsonData);

            www.uploadHandler = new UploadHandlerRaw(jsonDataBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Room", GameManager.Instance.GetRoomSet());
            
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                PieceManager.Instance.MovePiece(col);
                
                BoardManager.Instance.ClearCell();
            }
            else
            {
                print($"Error! : {www.error}");
            }
        }
    }    
}