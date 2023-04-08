using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xCoord;
    public int yCoord;
    private bool robberPresent;
    public ResourceType type;
    public int num;

    public Tile(int xCoord, int yCoord, ResourceType type){
        // if terrain = desert, robberPresent = true; else false;
        this.xCoord = xCoord;
        this.yCoord = yCoord;
        this.type = type;
    }

    public void OnMouseDown(){
        Controller controller = GameObject.Find("GameController").GetComponent<Controller>();
        if(controller.robberMode){
            controller.robberMode = false;
            controller.activateRobber(controller.currentPlayer, this);
        }
        Debug.Log("Tile clicked.");
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
        robberPresent = (num == -1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
