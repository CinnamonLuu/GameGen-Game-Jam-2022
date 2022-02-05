using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{

    public GameObject a;


    private const int GridCols = 9;
    private const int GridRows = 8;

    private const int StartX = 3;
    private const int StartY = 5;

    private Vector2 BaseRoomSize;

    private GridCell[,] _grid = new GridCell[GridRows, GridCols];
    private List<DungeonRoom> _nonOccupiedCells = new List<DungeonRoom>();
    private DungeonRoom[,] _floor = new DungeonRoom[GridRows, GridCols];


    private List<GridCell> _open = new List<GridCell>();
    private List<DungeonRoom> _endRooms = new List<DungeonRoom>();

    //Special Rooms
    private DungeonRoom _bossRoom;
    private DungeonRoom _secretRoom;
    private DungeonRoom _treasureRoom;

    public int Level = 1;


    private int MaxNumRooms => (int)(Random.Range(0, 2) + 5 + Level * 2.6);
    private int MinNumRooms => (int)(Random.Range(0, 2) + 5 + 1 * 2.6);

    private int _generatedRooms = 0;

    // Start is called before the first frame update
    void Start()
    {

        BaseRoomSize = new Vector2(a.GetComponent<Renderer>().bounds.size.x, a.GetComponent<Renderer>().bounds.size.y);
        //initialize map

        for (int i = 0; i < GridRows; i++)
        {
            for (int j = 0; j < GridCols; j++)
            {
                _grid[i, j] = new GridCell(i, j);
                _nonOccupiedCells.Add(new DungeonRoom(_grid[i, j], DistanceFromStart(i, j)));

            }
        }


        Expand(StartX, StartY);
        while (_open.Count != 0)
        {

            int x = _open[0].X;
            int y = _open[0].Y;
            _open.RemoveAt(0);
            bool created = false;

            if (x > 1) created = created | Expand(x - 1, y);
            if (x < GridRows - 1) created = created | Expand(x + 1, y);
            if (y > 2) created = created | Expand(x, y - 1);
            if (y < GridCols - 1) created = created | Expand(x, y + 1);

            if (!created)
                _endRooms.Add(_floor[x, y]);

        }
        _endRooms.Sort((a, b) => a.DistanceFromStart.CompareTo(b.DistanceFromStart));


        //Set treasure room 
        //TODO: move to PlaceRoom(BOOS_ROOM, Position)
        _bossRoom = _endRooms[_endRooms.Count - 1];
        _bossRoom.IsBossRoom = true;
        GameObject boss = Instantiate(a, new Vector3(_bossRoom.LocationInMap.X * BaseRoomSize.x, _bossRoom.LocationInMap.Y * BaseRoomSize.y, -1), a.transform.rotation);
        boss.GetComponent<MeshRenderer>().material.color = Color.red;

        //Set treasure room 
        //TODO: move to PlaceRoom(TREASURE_ROOM, Position)
        _treasureRoom = _endRooms[_endRooms.Count - 2];
        _treasureRoom.IsTreasureRoom = true;
        GameObject treasure = Instantiate(a, new Vector3(_treasureRoom.LocationInMap.X * BaseRoomSize.x, _treasureRoom.LocationInMap.Y * BaseRoomSize.y, -1), a.transform.rotation);
        treasure.GetComponent<MeshRenderer>().material.color = Color.yellow;



    }

    private bool Expand(int x, int y)
    {
        if (_grid[x, y].IsExplored)
            return false;

        int neighboursCreated = GetCreatedNeighbours(x, y);

        if (neighboursCreated > 1)
            return false;

        if (_generatedRooms >= MaxNumRooms)
            return false;

        //Random chance to create a room, besides the initial room that will always generate
        if (Random.Range(0, 2) < 0.5f && (x != 3 && y != 5))
        {
            return false;
        }


        _open.Add(_grid[x, y]);
        _grid[x, y].IsExplored = true;

        DungeonRoom room = _nonOccupiedCells.Find(room => (room.LocationInMap.X == x && room.LocationInMap.Y == y));
        _floor[x, y] = room;
        _nonOccupiedCells.Remove(room);

        _generatedRooms += 1;

        Instantiate(a, new Vector3(x * BaseRoomSize.x, y * BaseRoomSize.y, 0f), a.transform.rotation);

        return true;
    }

    private int DistanceFromStart(int x, int y)
    {
        return Mathf.Abs(StartX - x) + Mathf.Abs(StartY - y);
    }

    private int GetCreatedNeighbours(int x, int y)
    {
        GridCell[] neighbours = new GridCell[4];
        GridCell currentCell = _grid[x, y];

        neighbours[0] = (currentCell.X > 0) ? _grid[x - 1, y] : null;
        neighbours[1] = (currentCell.Y > 0) ? _grid[x, y - 1] : null;
        neighbours[2] = (currentCell.Y < GridCols - 1) ? _grid[x, y + 1] : null;
        neighbours[3] = (currentCell.X < GridRows - 1) ? _grid[x + 1, y] : null;

        int anyExplored = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null && neighbours[i].IsExplored)
            {
                anyExplored += 1;
            }
        }

        return anyExplored;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaceRoom();
        }
    }

    private void PlaceRoom()
    {

        //Calculate numNeighbours for each created room
        foreach (DungeonRoom room in _nonOccupiedCells)
        {
            room.NumNeighbours = GetCreatedNeighbours(room.LocationInMap.X, room.LocationInMap.Y);
        }

        _nonOccupiedCells.Sort((a, b) => b.NumNeighbours.CompareTo(a.NumNeighbours));

        int posX = _nonOccupiedCells[0].LocationInMap.X, posY = _nonOccupiedCells[0].LocationInMap.Y;

        _floor[posX, posY] = new DungeonRoom(_grid[posX, posY], DistanceFromStart(posX, posY));
        _secretRoom = _floor[posX, posY];
        _secretRoom.IsSecretRoom = true;
        GameObject treasure = Instantiate(a, new Vector3(posX * BaseRoomSize.x, posY * BaseRoomSize.y, -1f), a.transform.rotation);
        treasure.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}