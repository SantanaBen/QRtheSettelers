using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    // All intersections are adjacent to three tiles.

    public Tile t1;
    public Tile t2;
    public Tile t3;
    public bool settlementPresent = false;
    public bool cityPresent = false;
    public bool roadPresent = false;
    public bool selected = false;
    public List<Road> roads = new List<Road>();
    public Settlement settlement;
    public City city;

    public void OnMouseDown(){
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();

        if(controller.settlementBuildMode){
            Debug.Log("Intersection selected for settlement build mode.");
            controller.buildSettlement(controller.currentPlayer, this);
        } else if (controller.cityBuildMode) {
            Debug.Log("Intersection selected for city build mode.");
            controller.buildCity(controller.currentPlayer, this);
        } else if (controller.roadBuildingMode){
            Debug.Log("Intersection selected for road build mode.");
            selected = true;

            List<Intersection> intersections = GameBoard.instance.intersections;
            foreach(Intersection i in intersections){
                if(i.selected && i != this){
                    controller.buildRoad(controller.currentPlayer, i, this);
                    i.selected = false;
                    selected = false;
                    break;
                }
            }
        } else {
            Debug.Log("Intersection clicked. Not in any build mode.");
        }
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
