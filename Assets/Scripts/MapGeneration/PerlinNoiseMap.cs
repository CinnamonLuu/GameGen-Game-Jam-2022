using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{

    Dictionary<int, GameObject> _tileset;
    Dictionary<int, GameObject> _tileGroups;

    public List<GameObject> elementsInPerlin;

    int[,] _noiseGrid;
    GameObject[,] _tileGrid;
    public Room parentRoom;

    [SerializeField,Range(4f,20f)]
    float _magnification = 7f;

    [SerializeField]
    public int XOffset = 0; //moving left when decreasing, right when increasing
    [SerializeField]
    public int YOffset = 0; //moving down when decreasing, up when increasing


    // Start is called before the first frame update
    void Start()
    {
        CreateTileSet();
        CreateTileGroup();

        GeneratePerlinMap();
    }

    private void GeneratePerlinMap()
    {
        _noiseGrid = new int[Room.RoomRows, Room.RoomCols];
        _tileGrid = new GameObject[Room.RoomRows, Room.RoomCols];

        for (int i = 0; i < Room.RoomRows; i++)
        {

            for (int j = 0; j < Room.RoomCols; j++)
            {
                int tileID = GetIDUsingPerlin(i, j);
                _noiseGrid[i, j] = tileID;
                CreateTile(tileID,i,j);
            }
        }
    }

    private void CreateTile(int tileID, int x, int y)
    {
        GameObject tilePrefab = _tileset[tileID];
        GameObject tileGroup = _tileGroups[tileID];
        GameObject tile = Instantiate(tilePrefab, tileGroup.transform);

        tile.name = string.Format("tile: x{0} y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, -0.1f) * Room.CellSize;

        _tileGrid[x, y] = tile;

    }

    private int GetIDUsingPerlin(int x, int y)
    {
        float rawPerlinValue=  Mathf.PerlinNoise((x - XOffset)/_magnification, (y-YOffset)/_magnification);
        float clampedPerlinValue = Mathf.Clamp01(rawPerlinValue);

        float scaledPerlin = clampedPerlinValue * _tileset.Count;

        if (scaledPerlin == _tileset.Count)
            scaledPerlin = _tileset.Count - 1;

        return Mathf.FloorToInt(scaledPerlin);
    }

    private void CreateTileGroup()
    {
        _tileGroups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> pair in _tileset)
        {
            GameObject tileGroup = new GameObject(pair.Value.name);
            tileGroup.transform.parent = gameObject.transform;
            tileGroup.transform.localPosition = Vector3.zero;
            _tileGroups.Add(pair.Key, tileGroup);
        }
    }

    private void CreateTileSet()
    {
        _tileset = new Dictionary<int, GameObject>();
        for (int i = 0; i < elementsInPerlin.Count; i++)
        {
            _tileset.Add(i, elementsInPerlin[i]);
        }
    }
}
