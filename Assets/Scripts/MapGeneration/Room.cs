using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{

    public const int RoomRows = 14;
    public const int RoomCols = 8;

    public int cellSize = 100;
    public GridCell LocationInMap { get; }

    public GridCell[,] _grid = new GridCell[RoomRows, RoomCols];

    public int DistanceFromStart { get; }
    public int NumNeighbours { get; set; }

    public bool IsBossRoom;
    public bool IsSecretRoom;
    public bool IsTreasureRoom;


    public Room(GridCell cell)
    {
        LocationInMap = cell;
    }
    public Room(GridCell cell, int distance)
    {
        LocationInMap = cell;
        DistanceFromStart = distance;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < RoomRows; i++)
        {
            for (int j = 0; j < RoomCols; j++)
            {
                Handles.Label(transform.position + new Vector3(i * cellSize + cellSize/2, j * cellSize + cellSize / 2, 0), i + "," + j);
            }
        }
    }

}
