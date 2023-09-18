using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class PieceManager : MonoBehaviour
    {
        [Header("검정 피스")][Space]
        public GameObject blackPawn;
        public GameObject blackBishop;
        public GameObject blackKnight;
        public GameObject blackQueen;
        public GameObject blackKing;
        public GameObject blackRook;
        [Header("흰색 피스")][Space]
        public GameObject whitePawn;
        public GameObject whiteBishop;
        public GameObject whiteKnight;
        public GameObject whiteQueen;
        public GameObject whiteKing;
        public GameObject whiteRook;
        [Space]
            
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
        
    }
}