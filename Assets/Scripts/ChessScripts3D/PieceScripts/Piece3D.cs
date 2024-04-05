using UnityEngine;

namespace ChessScripts3D.PieceScripts
{
    public class Piece3D : MonoBehaviour
    {
        public PieceData3D pieceData3D;
    
        private bool _isClick;
        private MeshRenderer _renderer;
        private Material _outlineMaterial;
        private Material _defaultMaterial;
    
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            var mat = _renderer.sharedMaterials;
            _outlineMaterial = mat[1];
            _defaultMaterial = mat[0];
            HighlightOff();
        }

        public bool CheckIsClick(bool isClicked)
        {
            _isClick = isClicked;

            if (_isClick)
                HighlightOn();
            else
                HighlightOff();

            return _isClick;
        }
        
        private void HighlightOff()
        {
            Material[] mats = _renderer.sharedMaterials;
            mats[1] = _defaultMaterial;
            _renderer.sharedMaterials = mats;
        }
        
        private void HighlightOn()
        {
            Material[] mats = _renderer.sharedMaterials;
            mats[1] = _outlineMaterial;
            _renderer.sharedMaterials = mats;
        }
    }
}
