using System;
using UnityEditor;
using UnityEngine;

namespace Pieces
{
    [Serializable]
    public class Piece : MonoBehaviour
    {
        public int id;
        public int x;
        public int y;
        public bool hasMoved;
        public string pieceColor;

        private bool _isClick;
        private Material _material;

        void Start()
        {
            _material = GetComponent<Material>();
        }
        
        public void InitPiece(int _id, int _x, int _y, string color)
        {
            id = _id;
            x = _x;
            y = _y;
            pieceColor = color;
        }

        public int GetPieceId()
        {
            return id;
        }
        
        public Vector2 GetPiecePosition()
        {
            return new Vector2(x, y);
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
            var property = "_Alpha";
            _material.SetFloat(property, 0);
        }
        
        private void HighlightOn()
        {
            var property = "_Alpha";
            _material.SetFloat(property, 0.5f);
        }
    }
}
