using System;
using System.Collections.Generic;
using ChessScripts3D.Socket;
using Cinemachine;
using UnityEngine;

namespace ChessScripts3D.BoardScrips
{
    public class Board3D : MonoBehaviour
    {
        [SerializeField] private Transform black;
        [SerializeField] private Transform neutral;
        [SerializeField] private Transform white;

        [SerializeField] private Transform queenLayers;
        [SerializeField] private Transform kingLayers;

        public List<BoardCell3D> boardCellList = new List<BoardCell3D>();
        
        public GameObject BoardCell
        {
            get => boardCell;
            private set => boardCell = value;
        }

        [SerializeField] private GameObject boardCell;
        [SerializeField] private float cellSize = 1.5f;

        private int _countFile = 1;
        private int _countRank = 1;
        private int _mainInitCount;
        private int _attackInitCount;
        private Level _level;

        public void MainBoardInit()
        {
            // white -> neutral -> black 순서는 절대적 !
            _level = Level.White;
            CreateMainGrid(white);

            _level = Level.Neutral;
            CreateMainGrid(neutral);

            _level = Level.Black;
            CreateMainGrid(black);
        }

        public void AttackBoardInit()
        {
            _countFile = 0;
            for (int i = 0; i < queenLayers.childCount; i++)
            {
                _level = Enum.Parse<Level>("QL" + (i+1));
                CreateAttackGrid(queenLayers.GetChild(i));
            }
            
            _countFile = 4;
            for (int i = 0; i < kingLayers.childCount; i++)
            {
                _level = Enum.Parse<Level>("KL" + (i+1));
                CreateAttackGrid(kingLayers.GetChild(i));
            }
        }
        
        private void CreateMainGrid(Transform board)
        {
            var boardPos = board.position;
            var pivotPadding = -2.25f;

            for (int x = 0; x < 4; x++)
            {
                for (int z = 3; z >= 0; z--)
                {
                    Vector3 gridPivotPos = new Vector3(boardPos.x + (cellSize * x) + pivotPadding,
                        boardPos.y,
                        boardPos.z + (cellSize * z) + pivotPadding);

                    CreateCell(gridPivotPos, board);

                    MainBoardFileCounter();
                }
                _countRank++;
            }
            MainBoardRankCounter();
        }

        private void CreateAttackGrid(Transform board)
        {
            var boardPos = board.position;
            var pivotPadding = -0.75f;

            for (int x = 0; x < 2; x++)
            {
                for (int z = 1; z >= 0; z--)
                {
                    Vector3 gridPivotPos = new Vector3(boardPos.x + (cellSize * x) + pivotPadding
                        , boardPos.y
                        , boardPos.z + (cellSize * z) + pivotPadding);

                    CreateCell(gridPivotPos, board);

                    AttackBoardFileCounter();
                }
                _countRank++;
            }
            AttackBoardRankCounter();
        }

        private void CreateCell(Vector3 cellPos, Transform board)
        {
           var cell = Instantiate(boardCell, cellPos, Quaternion.Euler(board.eulerAngles), board);
           var cellProperty = cell.GetComponent<BoardCell3D>();

           cellProperty.File = Enum.Parse<File>(Enum.GetName(typeof(File), _countFile));
           cellProperty.Rank = Enum.Parse<Rank>(Enum.GetName(typeof(Rank), _countRank));
           cellProperty.Level = _level;
           
           boardCellList.Add(cell.GetComponent<BoardCell3D>());
        }

        private void MainBoardFileCounter()
        {
            if (_countFile == 4)
            {
                _countFile = 1;
                return;
            }
            _countFile++;
        }

        private void AttackBoardFileCounter()
        {
            if (_level < (Level)6 && _countFile == 1)
            {
                _countFile = 0;
                return;
            }
            
            if (_level > (Level)8 && _countFile == 5)
            {
                _countFile = 4;
                return;
            } 
            _countFile++;
        }

        private void MainBoardRankCounter()
        {
            _mainInitCount++;

            if (_mainInitCount == 1)
            {
                _countRank = 3; 
                return;
            }
            if (_mainInitCount == 2)
            {
                _countRank = 5; 
                return;
            }
            if (_mainInitCount == 3)
                _countRank = 0;
        }

        private void AttackBoardRankCounter()
        {
            if (_countRank is 2 or 4 || (_countRank == 6 && _attackInitCount == 1))
            {
                _countRank += 2;
                return;
            }
            
            if (_attackInitCount == 0)
            {
                _attackInitCount++;
            }
            if (_countRank is 6 or 8)
            {
                _countRank -= 4;
                return;
            }

            if (_countRank != 10) return;
            _countRank = 0;
            _attackInitCount = 0;
        }
    }
}
