using UnityEngine;
using WarpSquareEngine;

namespace ChessScripts3D.BoardScrips
{
    public class MainBoard3D : MonoBehaviour, IBoard3D
    {
        public Level level;
        
        public void InitBoard(Level lev)
        {
            level = lev;
            transform.position = GetWorldSpaceTransform();

            Game s = new Game();
        }

        public Vector3 GetWorldSpaceTransform()
        {
            Vector3 boardPosition;
            
            switch (level)
            {
                case Level.White:
                    boardPosition = new Vector3(-3, -5);        
                    return boardPosition;
                case Level.Black:
                    boardPosition = new Vector3(3, 5);
                    return boardPosition;
                case Level.Neutral:
                    boardPosition = Vector3.zero;
                    return boardPosition;
            }
            
            return Vector3.zero;
        }
    }
}