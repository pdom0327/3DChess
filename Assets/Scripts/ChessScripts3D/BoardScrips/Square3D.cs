using UnityEngine;
using WarpSquareEngine;

namespace ChessScripts3D.BoardScrips
{
    public class Square3D : MonoBehaviour
    {
        public File file;
        public Rank rank;
        public Level level;

        public void InitSquare(Square square)
        {
            file = square.GetFile();
            rank = square.GetRank();
            level = square.GetLevel();
        }
    }
}
