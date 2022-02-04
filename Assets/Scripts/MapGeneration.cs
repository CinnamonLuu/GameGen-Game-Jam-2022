using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    private const int GridCols = 9;
    private const int GridRows = 8;
    private GridCell[,] _grid = new GridCell[GridRows, GridCols];
    
    private List<GridCell> open = new List<GridCell>();

    public int Level = 1;
    public GameObject a;

    [SerializeField] private int _maxNumRooms => (int) (Random.Range(0, 2) + 5 + Level * 2.6);
    private int exploredRooms = 0;

    // Start is called before the first frame update
    void Start()
    {
        //initialize map
        int v = 1;
        string line = "";
        for (int i = 0; i < GridRows; i++)
        {
            for (int j = 0; j < GridCols; j++)
            {
                _grid[i, j] = new GridCell(i, j);

                line += (v.ToString() + ", ");
                v++;
            }

            v++;
            line += "\n";
        }


        expand(3, 5);
        while (open.Count != 0)
        {
            
            int x = open[0].X;
            int y = open[0].Y;
            open.RemoveAt(0);
            bool created = false;

            if (x > 1) created = created | expand(x - 1, y);
            if (x < GridRows-1) created = created | expand(x + 1, y);
            if (y > 2) created = created | expand(x, y - 1);
            if (y < GridCols-1) created = created | expand(x, y + 1);


        }
        //Debug.Log(line);
    }

    private bool expand(int x, int y)
    {
        if (_grid[x, y].IsExplored)
            return false;

        int neighboursExplored = getNeighbours(x, y);

        if (neighboursExplored > 1)
            return false;
        
        if(exploredRooms >= _maxNumRooms)
            return false;
        float b = Random.Range(0, 2);
        if (b < 0.5f && (x != 3 && y != 5))
        {
            Debug.Log(b);
            return false;
        }
        Debug.Log(b);
        
        open.Add(_grid[x, y]);
        _grid[x, y].IsExplored = true;
        exploredRooms += 1;

        Instantiate(a, new Vector3(x, 0, y), Quaternion.identity);

        return true;
    }

    private int getNeighbours(int x, int y)
    {
        GridCell[] neighbours = new GridCell[4];
        GridCell currentCell = neighbours[0] = _grid[x, y];

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

    // Update is called once per frame
    void Update()
    {
    }
}