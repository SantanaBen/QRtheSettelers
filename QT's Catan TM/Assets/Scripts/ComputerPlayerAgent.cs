using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

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
        System.Random random = new System.Random();
        int index = random.Next(resourceList.Count);
        ResourceType card = resourceList[index];
        resources[card] -= 1;
        return card;
    }

    // Uses RNG to move the robber to a random board tile
    public override void moveRobber(GameBoard board)
    {
        // Removes robber from current tile
        foreach(Tile t in board.getBoard().Values){
            if(t.getRobberPresent()){
                t.toggleRobber();           
            }
        }

        System.Random random = new System.Random();
        List<Tile> tiles = board.getBoard().Values.ToList();
        int index = random.Next(tiles.Count);
        // Moves the robber to a randomly chosen tile
        Tile newRobberTile = tiles[index];
        newRobberTile.toggleRobber();
    }

    public bool canBuildSettlement(){
        return(resources[ResourceType.Brick] > 0 &&
               resources[ResourceType.Lumber] > 0 &&
               resources[ResourceType.Grain] > 0 &&
               resources[ResourceType.Wool] > 0);
    }

    public bool canBuildRoad(){
        return(resources[ResourceType.Brick] > 0 &&
               resources[ResourceType.Lumber] > 0
        );
    }
}