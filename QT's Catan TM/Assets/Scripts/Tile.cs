using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xCoord;
    public int yCoord;
    private bool robberPresent;
    private ResourceType type;
    public int num;

    // private TerrainType terrain;

    public Tile(int xCoord, int yCoord, ResourceType type){
        // if terrain = desert, robberPresent = true; else false;
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        this.type = type;
    }

    public void toggleRobber(){
        robberPresent = !robberPresent;
    }

    public int getXCoord(){
        return this.xCoord;
    }

    public int getYCoord(){
        return this.yCoord;
    }

    public bool getRobberPresent(){
        return robberPresent;
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
