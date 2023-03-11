using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private List<Tile> board = List<Tile>(19);

    public void addTile(Tile tile){
        board.Add(tile);
    }

    public List<Tile> getBoard(){
        return board;
    }

    void setUpBoard(){
        // Enter logic for setting up board
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void getAdjacentTiles(Tile tile){
        // This is a method that will be used frequently in the game logic
        // List<Tiles> tiles = board.getTiles();
        // For each tile in tiles
        // increment and decrement y, getTile each time
        // Decrement and increment x, each time increment y once to getTile
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
