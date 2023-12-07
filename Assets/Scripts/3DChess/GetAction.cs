using System;
using System.Collections.Generic;
using _3DChess;
using UnityEngine;

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
