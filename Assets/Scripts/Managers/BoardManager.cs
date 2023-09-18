using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class BoardManager : MonoBehaviour
    {
        public List<BoardCell> boardCells;

        private Board _board;

        private static BoardManager _instance = null;

        public static BoardManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (BoardManager)FindObjectOfType(typeof(BoardManager));
                }
                return _instance;
            }
        }

        void Start()
        {
            _board ??= GameObject.FindGameObjectWithTag(nameof(Board)).GetComponent<Board>();
            
            _board.CreateGrid();
            PieceManager.Instance.SettingPiece();
        }
    }
}
