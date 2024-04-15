using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using WarpSquareEngine;

namespace ChessScripts3D.BoardScrips
{
    public interface IBoard3D
    {
        public void InitBoard(Level lev);
        public Vector3 GetWorldSpaceTransform();
    }
}
