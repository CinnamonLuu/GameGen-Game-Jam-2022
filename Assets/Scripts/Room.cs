using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public GridCell LocationInMap { get; }

    public int DistanceFromStart { get; }
    public int NumNeighbours { get; set; }

    public bool IsBossRoom;
    public bool IsSecretRoom;
    public bool IsTreasureRoom;


    public Room(GridCell cell)
    {
        LocationInMap = cell;
    }
    public Room(GridCell cell,int distance)
    {
        LocationInMap = cell;
        DistanceFromStart = distance;
    }


}
