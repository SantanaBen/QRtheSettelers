using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int xCoord;
    private int yCoord;
    private ResourceType type;
    // private TerrainType terrain;

    public Tile(int xCoord, int yCoord, ResourceType type){
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        this.type = type;
    }

    public int getXCoord(){
        return this.xCoord;
    }

    public int getYCoord(){
        return this.yCoord;
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
