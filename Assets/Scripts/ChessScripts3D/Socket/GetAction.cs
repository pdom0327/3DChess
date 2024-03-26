using System;
using System.Collections.Generic;
using WarpSquareEngine;

namespace ChessScripts3D.Socket
{
    [Serializable]
    public enum Action
    {
        INIT,
        COLOR,
    }

    [Serializable]
    public class GetActionColor
    {
        public Action action;
        public string color;
    }

    [Serializable]
    public class GetActionInit
    {
        public Action action;
        public List<InitData> locationList;
    }
}