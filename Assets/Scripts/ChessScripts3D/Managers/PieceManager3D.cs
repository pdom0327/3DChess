using System;
using System.Collections.Generic;
using ChessScripts3D.PieceScripts;
using ChessScripts3D.Socket;
using Action = ChessScripts3D.Socket.Action;

namespace ChessScripts3D.Managers
{
    public class PieceManager3D : SingleTon<PieceManager3D>
    {
        public PieceInit3D init;
        public PieceDataBase3D dataBase;

        private Action _action;
        
        public string color;
        private bool IsInit;
        private List<InitData> initData;

        private void Awake()
        {
            init = GetComponent<PieceInit3D>();
            dataBase = GetComponent<PieceDataBase3D>();
        }

        void Update()
        {
            if (IsInit && _action == Action.INIT)
            {
                IsInit = false;
                init.SetClickMask(color);
                init.InitPiece(initData, dataBase);
            }
        }

        public void SetColor(GetActionColor data)
        {
            _action = data.action;
            color = data.color;
        }
        public void SetInit(GetActionInit data)
        {
            _action = data.action;
            IsInit = true;
            initData = data.locationList;
        }
    }
}
