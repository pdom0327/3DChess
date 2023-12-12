using ChessScripts3D.BoardScrips;

namespace ChessScripts3D.Managers
{
    public class BoardManager3D : SingleTon<BoardManager3D>
    {
        public Board3D board;
        void Start()
        {
            board.MainBoardInit();
            board.AttackBoardInit();
        }
    }
}
