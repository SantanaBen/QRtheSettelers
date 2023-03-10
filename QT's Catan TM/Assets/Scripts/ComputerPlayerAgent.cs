using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayerAgent : Player
{

    public ComputerPlayerAgent(string name, bool isComputer, string color) 
    : 
    base(name, isComputer, color)
    {

    }

     public override ResourceType giveUpResource(){
        List<ResourceType> resourceList = new List<ResourceType>();
        foreach(KeyValuePair<ResourceType, int> kvp in resources){ //Creates a list of resources to randomly choose from
            for(int i = 0; i < kvp.Value; i++){
                resourceList.Add(kvp.Key);
            }
        }
        Random random = new Random();
        int index = random.Next(resourceList.Count);
        ResourceType card = resourceList[index];
        resources[card] -= 1;
        return card;
    }

    // Uses RNG to move the robber to a random board tile
    public override void moveRobber(GameBoard board)
    {
        // Removes robber from current tile
        foreach(Tile t in board.getBoard()){
            if(t.getRobberPresent){
                t.toggleRobber();           
            }
        }

        Random random = new Random();
        List<Tile> tiles = board.getBoard();
        int index = random.Next(tiles.Count);
        // Moves the robber to a randomly chosen tile
        Tile newRobberTile = tiles[index];
        newRobberTile.toggleRobber();
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
