using System;
using System.Collections.Generic;
using WarpSquareEngine;

namespace ChessScripts3D.Socket
{
    [Serializable]
    public enum SocketAction
    {
        INIT,
        COLOR,
        MATCHED_USER,
    }

    [Serializable]
    public class GetAction
    {
        public SocketAction action;
    }
    
    [Serializable]
    public class GetColorAction : GetAction 
    {
        public Color color;
    }

    [Serializable]
    public class GetInitAction : GetAction
    {
        public List<InitData> locationList;
    }
}