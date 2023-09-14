using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public GameObject piece;
    public Vector2 cellPosition;
    [Space]
    public bool canMove;
}
