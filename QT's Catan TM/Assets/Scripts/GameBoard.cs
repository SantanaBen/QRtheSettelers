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

    // Update is called once per frame
    void Update()
    {
        
    }
}
