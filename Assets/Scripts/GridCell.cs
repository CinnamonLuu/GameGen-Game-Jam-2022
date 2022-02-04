using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public int X { get; }

    public int Y { get; }

    public bool IsExplored { get; set; } = false;

    public GridCell(int x, int y)
    {
        X = x;
        Y = y;

    }
}
