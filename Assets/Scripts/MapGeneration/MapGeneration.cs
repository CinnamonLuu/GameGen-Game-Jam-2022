using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    private const int GridCols = 9;
    private const int GridRows = 8;

    private const int StartX = 3;
    private const int StartY = 5;
    
    private GridCell[,] _grid = new GridCell[GridRows, GridCols];
    private List<Room>_nonOccupiedCells = new List<Room>();
    private Room[,] _floor = new Room[GridRows, GridCols];

    
    private List<GridCell> _open = new List<GridCell>();
    private List<Room> _endRooms = new List<Room>();
    
    //Special Rooms
    private Room _bossRoom;
    private Room _secretRoom;
    private Room _treasureRoom;

    public int Level = 1;
    public GameObject a;
    public GameObject b;
    
    
    private int MaxNumRooms => (int) (Random.Range(0, 2) + 5 + Level * 2.6);
    private int MinNumRooms => (int) (Random.Range(0, 2) + 5 + 1 * 2.6);
    
    private int _generatedRooms = 0;

    // Start is called before the first frame update
    void Start()
    {
        //initialize map

        for (int i = 0; i < GridRows; i++)
        {
            for (int j = 0; j < GridCols; j++)
            {
                _grid[i, j] = new GridCell(i, j);
                _nonOccupiedCells.Add(new Room(_grid[i, j], DistanceFromStart(i, j)));

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
            if (x < GridRows-1) created = created | Expand(x + 1, y);
            if (y > 2) created = created | Expand(x, y - 1);
            if (y < GridCols-1) created = created | Expand(x, y + 1);

            if (!created)
                _endRooms.Add(_floor[x,y]);

        }
        _endRooms.Sort((a,b) => a.DistanceFromStart.CompareTo(b.DistanceFromStart));


        //Set treasure room 
        //TODO: move to PlaceRoom(BOOS_ROOM, Position)
        _bossRoom = _endRooms[_endRooms.Count - 1];
        _bossRoom.IsBossRoom = true;
        GameObject boss = Instantiate(b, new Vector3(_bossRoom.LocationInMap.X, 1, _bossRoom.LocationInMap.Y), Quaternion.identity);
        boss.GetComponent<MeshRenderer>().material.color = Color.red;

        //Set treasure room 
        //TODO: move to PlaceRoom(TREASURE_ROOM, Position)
        _treasureRoom = _endRooms[_endRooms.Count - 2];
        _treasureRoom.IsTreasureRoom = true;
        GameObject treasure = Instantiate(b, new Vector3(_treasureRoom.LocationInMap.X, 1, _treasureRoom.LocationInMap.Y), Quaternion.identity);
        treasure.GetComponent<MeshRenderer>().material.color = Color.yellow;



    }

    private bool Expand(int x, int y)
    {
        if (_grid[x, y].IsExplored)
            return false;

        int neighboursCreated = GetCreatedNeighbours(x, y);

        if (neighboursCreated > 1)
            return false;
        
        if(_generatedRooms >= MaxNumRooms)
            return false;
        
        //Random chance to create a room, besides the initial room that will always generate
        if (Random.Range(0, 2) < 0.5f && (x != 3 && y != 5))
        {
            return false;
        }

        
        _open.Add(_grid[x, y]);
        _grid[x, y].IsExplored = true;

        Room room = _nonOccupiedCells.Find(room => (room.LocationInMap.X == x && room.LocationInMap.Y == y));
        _floor[x, y] = room;
        _nonOccupiedCells.Remove(room);

        _generatedRooms += 1;

        Instantiate(a, new Vector3(x, 0, y), Quaternion.identity);

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
            if (neighbours[i] != null && neighbours[i].IsExplored  )
            {
                anyExplored += 1;
            }
        }

        return anyExplored;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaceRoom();
        }
    }

    private void PlaceRoom()
    {

        //Calculate numNeighbours for each created room
        foreach (Room room in _nonOccupiedCells)
        {
            room.NumNeighbours = GetCreatedNeighbours(room.LocationInMap.X, room.LocationInMap.Y);
        }

        _nonOccupiedCells.Sort((a, b) => b.NumNeighbours.CompareTo(a.NumNeighbours));

        int posX = _nonOccupiedCells[0].LocationInMap.X, posY = _nonOccupiedCells[0].LocationInMap.Y;

        _floor[posX, posY] = new Room(_grid[posX, posY], DistanceFromStart(posX, posY));
        _secretRoom = _floor[posX, posY];
        _secretRoom.IsSecretRoom = true;
        GameObject treasure = Instantiate(a, new Vector3(posX, 0f, posY), Quaternion.identity);
        treasure.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}