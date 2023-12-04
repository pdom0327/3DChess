using UnityEngine;

namespace _3DChess.BoardScrips
{
    public class Board3D : MonoBehaviour
    {
        [SerializeField] private Transform black;
        [SerializeField] private Transform neutral;
        [SerializeField] private Transform white;

        [SerializeField] private Transform queenLayers;
        [SerializeField] private Transform kingLayers;
        
        public GameObject BoardCell
        {
            get => boardCell;
            private set => boardCell = value;
        }

        [SerializeField] private GameObject boardCell;
        [SerializeField] private float cellSize = 1.5f;
        
        void Start()
        {
            CreateMainGrid(black);
            CreateMainGrid(white);
            CreateMainGrid(neutral);
            
            for (int i = 0; i < queenLayers.childCount; i++)
            {
                CreateAttackGrid(queenLayers.GetChild(i));
            }

            for (int i = 0; i < kingLayers.childCount; i++)
            {
                CreateAttackGrid(kingLayers.GetChild(i));
            }
        }

        private void CreateMainGrid(Transform board)
        {
            var boardPos = board.position;
            var gridPivot = -2.25f;

            for (int x = 0; x < 4; x++)
            {
                for (int z = 3; z >= 0; z--)
                {
                    Vector3 gridPos = new Vector3(boardPos.x + (cellSize * x) + gridPivot,
                        boardPos.y,
                        boardPos.z + (cellSize * z) + gridPivot);     
                    Instantiate(boardCell, gridPos, Quaternion.Euler(board.eulerAngles), board);
                }
            }
        }

        private void CreateAttackGrid(Transform board)
        {
            var boardPos = board.position;
            var gridPivot = -0.75f;

            for (int x = 0; x < 2; x++)
            {
                for (int z = 1; z >= 0; z--)
                {
                    Vector3 gridPos = new Vector3(boardPos.x + (cellSize * x) + gridPivot
                        , boardPos.y
                        , boardPos.z + (cellSize * z) + gridPivot);
                        
                    Instantiate(boardCell, gridPos, Quaternion.Euler(board.eulerAngles), board);
                }
            }    
        }
    }
}
