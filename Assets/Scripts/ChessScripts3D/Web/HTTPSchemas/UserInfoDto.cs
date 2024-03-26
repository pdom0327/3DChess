using System;
using UnityEngine;

namespace ChessScripts3D.Web.HTTPSchemas
{
    [Serializable]
    public class UserInfoDto
    {
        public string userName;
        public Tier tier;
        public int score;
    }
    public enum Tier {
        Farmer,
        Soldier,
        Veterans,
        Deacon,
        Priests,
        HighPriests,
        Squire,
        Knight,
        Crusader,
        General,
        Duke,
        Royal
    }
}