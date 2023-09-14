using UnityEngine;

namespace Managers
{
    public class PieceManager : MonoBehaviour
    {
        public GameObject blackPawn;

        private static PieceManager _instance;

        public static PieceManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (PieceManager)FindObjectOfType(typeof(PieceManager));
                }
                return _instance;
            }
        }
    
        void Start()
        {
            
        }
        
        public static void InitPiece()
        {
        
        }
        
        public void SettingPiece()
        {
            
            foreach (var cell in BoardManager.Instance.boardCells)
            {
                Vector3 posTank = cell.transform.position;
                Instantiate(blackPawn, posTank, Quaternion.Euler(transform.eulerAngles));
            }
            
        }

        private void CheckID()
        {
            
        }
    }
    
    
}