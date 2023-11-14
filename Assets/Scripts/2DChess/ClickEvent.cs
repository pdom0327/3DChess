using Boards;
using HttpRequest;
using Managers;
using Pieces;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClickEvent : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private LayerMask pieceMask;
        [SerializeField] private LayerMask cellMask;
        private LayerMask _mask;
        
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
            GameManager gameManager = GameManager.Instance;
            
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 20f;
            
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(ray, out var hit, mousePosition.z, pieceMask)) return;
            
            var piece = hit.collider.GetComponent<Piece>();
            var pieceId = piece.pieceData.id;
            
            StartCoroutine(ClickRequest.Instance.GetPoint(pieceId));
            
            if (gameManager.clickedPiece)
            {
                //if (boardManager.activatedCells.Count == 0) return ;
                gameManager.clickedPiece.CheckIsClick(false);
                BoardManager.Instance.ClearCell();
            }

            piece.CheckIsClick(true);
            gameManager.clickedPiece = piece;
        }

        public void ClickCell()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 20f;
            
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            
            if (!Physics.Raycast(ray, out var hit, mousePosition.z, cellMask)) return;

            StartCoroutine(ClickRequest.Instance.PostMovePiece(hit.collider));
        }
        
    }
}