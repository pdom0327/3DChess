using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

namespace HttpRequest
{
    public class ClickRequest : MonoBehaviour
    {
        private string _url;
        
        private Camera _camera;

        private Piece.Piece _clickedPiece;
        
        void Start()
        {
            _url = $"https://heneinbackapi.shop/game/piece/valid-list";
            
            _camera = Camera.main;
        }
    
        void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 20f;

            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    _clickedPiece = hit.collider.GetComponent<Piece.Piece>();
                    StartCoroutine(GetPoint());
                }
            }
        }

        private IEnumerator GetPoint()
        {
            using(UnityWebRequest www = UnityWebRequest.Get($"{_url}/{_clickedPiece.id}"))
            {
                www.SetRequestHeader("Room", GameManager.Instance.GetRoomSet());
                
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.downloadHandler.text);
                }
                else
                {
                    Debug.Log($"Error! : {www.error}");
                }
            }
        }
    }    
}