using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom 
{
    public GridCell LocationInMap { get; }

    public int DistanceFromStart { get; }
    public int NumNeighbours { get; set; }

    public bool IsBossRoom;
    public bool IsSecretRoom;
    public bool IsTreasureRoom;


    public DungeonRoom(GridCell cell)
    {
        LocationInMap = cell;
    }
    public DungeonRoom(GridCell cell,int distance)
    {
        LocationInMap = cell;
        DistanceFromStart = distance;
    }


}
