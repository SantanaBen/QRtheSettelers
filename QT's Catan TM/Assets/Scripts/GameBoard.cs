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
    public Tile currentRobberTile;

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
        Controller controller = GameObject.Find("GameController").GetComponent<Controller>();
        // On board loaded, each player should place one settlement and road attached to it
        bool validatePair(Intersection i1, Intersection i2){
            if(i1.settlementPresent || i2.settlementPresent){
                return false;
            }
            if(!controller.verifyRoadLocation(i1, i2)){
                return false;
            }
            return true;
        }

        foreach(Player p in controller.players){

            int randomIndex = UnityEngine.Random.Range(0,instance.intersections.Count);
            Intersection i1 = instance.intersections[randomIndex];
            randomIndex = UnityEngine.Random.Range(0,instance.intersections.Count);
            Intersection i2 = instance.intersections[randomIndex];
            while(!(validatePair(i1, i2))){
                randomIndex = UnityEngine.Random.Range(0,instance.intersections.Count);
                i1 = instance.intersections[randomIndex];
                randomIndex = UnityEngine.Random.Range(0,instance.intersections.Count);
                i2 = instance.intersections[randomIndex];
            }
            controller.buildSettlement(p, i1);
            controller.buildRoad(p,i1, i2);
            // Give resources back and deduct VP
            p.victoryPoints--;
            p.resources[ResourceType.Lumber]+=2;
            p.resources[ResourceType.Brick]+=2;
            p.resources[ResourceType.Wool]+=1;
            p.resources[ResourceType.Grain]+=1;

        }
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
