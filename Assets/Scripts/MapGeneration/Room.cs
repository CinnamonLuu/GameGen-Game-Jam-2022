using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{

    private const int roomRows = 14;
    private const int roomCols = 8;
    
    private const int cellSize = 10;
    public static int RoomRows => roomRows;
    public static int RoomCols => roomCols;
    public static int CellSize  => cellSize;

    public GridCell LocationInMap;

    public GridCell[,] _grid = new GridCell[RoomRows, RoomCols];

    public PerlinNoiseMap perlin;

    public int DistanceFromStart { get; }
    public int NumNeighbours { get; set; }

    public bool IsBossRoom;
    public bool IsSecretRoom;
    public bool IsTreasureRoom;

    private void Start()
    {
        //perlin = GetComponent<PerlinNoiseMap>();
        perlin.XOffset = (LocationInMap.X * cellSize) + Random.Range(0,100);
        perlin.YOffset = LocationInMap.Y * cellSize + Random.Range(0, 100);

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < RoomRows; i++)
        {
            for (int j = 0; j < RoomCols; j++)
            {
                Handles.Label(transform.position + new Vector3(i * CellSize + CellSize/2, j * CellSize + CellSize / 2, 0), i + "," + j);
            }
        }
    }

}
