using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridCell
{
    [field: SerializeField]
    public int X;

    [field: SerializeField]
    public int Y;

    public bool IsExplored { get; set; } = false;

    public GridCell(int x, int y)
    {
        X = x;
        Y = y;

    }
}
