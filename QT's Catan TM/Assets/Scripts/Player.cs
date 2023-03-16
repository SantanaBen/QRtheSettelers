using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public string name;
    public bool cpu;
    public string colour;
    public int victoryPoints;
    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    public List<Settlement> settlements;
    public List<City> cities;
    public List<Road> roads;

    public Player(string name, bool cpu, string colour){
        this.name = name;
        this.cpu = cpu;
        this.colour = colour;
        victoryPoints = 0;
        settlements = new List<Settlement>();
        cities = new List<City>();
        roads = new List<Road>();

        resources.Add(ResourceType.Lumber, 0);
        resources.Add(ResourceType.Brick, 0);
        resources.Add(ResourceType.Wool, 0);
        resources.Add(ResourceType.Grain, 0);
        resources.Add(ResourceType.Ore, 0);
    }

    public virtual ResourceType giveUpResource(){
        // User selects a resource to give
        // resources[type] --;
        return ResourceType.Ore;
    }

    public virtual void moveRobber(GameBoard board){
        // Player chooses a tile to move the robber to;
        // Old: tile.toggleRobber()
        // New: tile.toggleRobber()
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
