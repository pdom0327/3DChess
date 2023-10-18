using HttpRequest;
using Pieces;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClickEvent : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private LayerMask pieceMask;
        
        private static ClickEvent _instance;

        public static ClickEvent Instance
        {
            get
            {
                if (_instance is null) {
                    _instance = (ClickEvent)FindObjectOfType(typeof(ClickEvent));
                }   
                return _instance;
            }
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }

        public void ClickPiece()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 20f;
            
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit, mousePosition.z,pieceMask))
            {
                var pieceId = hit.collider.GetComponent<Piece>().GetPieceId();
                StartCoroutine(ClickRequest.Instance.GetPoint(pieceId));
            }
        }
    }
}