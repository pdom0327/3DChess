using System;

using _3DChess;
using UnityEngine;


[Serializable]
public class PieceData3D
{
    private PieceType _pieceType;
    public PieceType PieceType
    {
        get => _pieceType;
        set => _pieceType = value;
    }
    
    private Rank _rank;
    public Rank Rank
    {
        get => _rank;
        set => _rank = value;
    }

    private File _file;
    public File File
    {
        get => _file;
        set => _file = value;
    }

    private Level _level;
    public Level Level
    {
        get => _level;
        set => _level = value;
    }
}
