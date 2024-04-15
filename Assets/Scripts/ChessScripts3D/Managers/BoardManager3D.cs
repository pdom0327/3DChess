using System.Collections.Generic;
using ChessScripts3D.BoardScrips;
using UnityEngine;
using WarpSquareEngine;

namespace ChessScripts3D.Managers
{
    public class BoardManager3D : SingleTon<BoardManager3D>
    {
        [SerializeField] public MainBoard3D whiteBoard;
        [SerializeField] public MainBoard3D blackBoard;
        [SerializeField] public MainBoard3D neutralBoard;

        private List<IBoard3D> boards = new List<IBoard3D>();
        
        void Awake()
        {
            whiteBoard.level = Level.White;
            blackBoard.level = Level.Black;
            neutralBoard.level = Level.Neutral;
            
            
        }
    }
}
