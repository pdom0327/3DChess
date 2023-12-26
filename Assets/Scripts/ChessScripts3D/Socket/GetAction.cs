using System;
using System.Collections.Generic;

namespace ChessScripts3D.Socket
{
    [Serializable]
    public enum Action
    {
        INIT,
        COLOR,
        Temp,
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