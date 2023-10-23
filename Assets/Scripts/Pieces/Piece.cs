using System;
using UnityEngine;

namespace Pieces
{
    [Serializable]
    public class Piece : MonoBehaviour
    {
        private PieceData PieceData { set => pieceData = value; }
        public PieceData pieceData;

        public string pieceColor;

        private bool _isClick;
        private MeshRenderer _renderer;
        private Material _outlineMaterial;
        private Material _defaultMaterial;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _outlineMaterial = _renderer.sharedMaterials[1];
            _defaultMaterial = _renderer.sharedMaterials[0];
            HighlightOff();
        }

        public void InitPiece(int initId, int initX, int initY, string color)
        {
            pieceData.id = initId;
            pieceData.x = initX;
            pieceData.y = initY;
            pieceColor = color;
        }

        public Vector2 GetPiecePosition()
        {
            return new Vector2(pieceData.x, pieceData.y);
        }

        public bool CheckIsClick(bool isClicked)
        {
            _isClick = isClicked;

            if (_isClick)
            {
                HighlightOn();
            }
            else
            {
                HighlightOff();
            }
            
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
