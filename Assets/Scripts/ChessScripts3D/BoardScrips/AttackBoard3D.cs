using UnityEngine;
using WarpSquareEngine;

namespace ChessScripts3D.BoardScrips
{
    public class AttackBoard3D : MonoBehaviour, IBoard3D
    {
        public Level level;

        public void InitBoard(Level lev)
        {
            MoveBoard(lev);
            
        }

        public void MoveBoard(Level lev)
        {
            level = lev;
            transform.position = GetWorldSpaceTransform();
        }

        public Vector3 GetWorldSpaceTransform()
        {
            Vector3 boardPosition;
            
            switch (level)
            {
                case Level.Ql1:
                    boardPosition = new Vector3(-6, -2.5f, 3);        
                    return boardPosition;
                case Level.Ql2:
                    boardPosition = new Vector3(0, -2.5f, 3);
                    return boardPosition;
                case Level.Ql3:
                    boardPosition = new Vector3(-3, 2.5f, 3);
                    return boardPosition;
                case Level.Ql4:
                    boardPosition = new Vector3(3, 2.5f, 3);
                    return boardPosition;
                case Level.Ql5:
                    boardPosition = new Vector3(0, 7.5f, 3);
                    return boardPosition;
                case Level.Ql6:
                    boardPosition = new Vector3(6, 7.5f, 3);
                    return boardPosition;
                case Level.Kl1:
                    boardPosition = new Vector3(-6, -2.5f, -3);
                    return boardPosition;
                case Level.Kl2:
                    boardPosition = new Vector3(0, -2.5f, -3);
                    return boardPosition;
                case Level.Kl3:
                    boardPosition = new Vector3(-3, 2.5f, -3);
                    return boardPosition;
                case Level.Kl4:
                    boardPosition = new Vector3(3, 2.5f, -3);
                    return boardPosition;
                case Level.Kl5:
                    boardPosition = new Vector3(0, 7.5f, -3);
                    return boardPosition;
                case Level.Kl6:
                    boardPosition = new Vector3(6, 7.5f, -3);
                    return boardPosition;
            }
            
            return Vector3.zero;
        }
    }
}