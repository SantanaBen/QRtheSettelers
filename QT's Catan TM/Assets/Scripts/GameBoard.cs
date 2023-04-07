using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameBoard : MonoBehaviour
{
    public static GameBoard instance;
    public Dictionary<(int,int), Tile> board = new Dictionary<(int, int), Tile>(19);
    public List<Tile> tiles = new List<Tile>();
    public List<Intersection> intersections = new List<Intersection>();

    public void addTile(Tile tile){
        board[(tile.getXCoord(), tile.getYCoord())] = tile;
    }

    public Tile getTile(int x, int y){
        if (board.TryGetValue((x, y), out Tile tile)){
            return tile;
        } else {
            return null;
        }
    }

    public Dictionary<(int,int), Tile> getBoard(){
        return board;
    }

    void setUpBoard(){
        // Enter logic for setting up board
    }

    // Start is called before the first frame update
    void Start()
    {
    
        
    }
    void Awake()
{
    if (instance == null) {
        instance = this;
        DontDestroyOnLoad(gameObject);
    } else {
        Destroy(gameObject);
    }
}

    // Returns all adjacent tiles to a given point
    List<Tile> getAdjacentTiles(Tile tile){
        List<Tile> adjacentTiles = new List<Tile>();
        int x = tile.getXCoord();
        int y = tile.getYCoord();

        // Operations can be done on the x and y coordinates to get the six possible adjacent tiles.
        List<(int,int)> temp = new List<(int, int)>(6); // Holds all potential coordinates for adjacent tiles.
        temp.Add((x+1,y));
        temp.Add((x-1,y));
        temp.Add((x,y-1));
        temp.Add((x, y+1));
        temp.Add((x-1,y+1));
        temp.Add((x+1,y+1));

        // Checks each potential coordinate tuple for a tile present, adds to list if not null
        foreach((int,int) xy in temp){
            if(getTile(xy.Item1, xy.Item2) != null){
                adjacentTiles.Add(getTile(xy.Item1, xy.Item2));
            }
        }
        return adjacentTiles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
