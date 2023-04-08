using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class EventDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject boardObject =  GameObject.Find("GameBoard");
        DontDestroyOnLoad(boardObject);
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        TextMeshProUGUI textbox = GameObject.Find("CurrentPlayerBox").GetComponent<TextMeshProUGUI>();
        textbox.text = "Player: " + controller.currentPlayer.colour;
        checkForDiceRoll();
        checkForSettlement();
        checkForRoad();
        checkForCity();
    }

    void checkForDiceRoll(){
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.recentRoll != 0){
            GameObject.Find("RollButton").GetComponent<Button>().interactable = false;
            controller.collectResources(controller.recentRoll);
        }
    }

    void checkForSettlement(){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.settlementBuildMode){
            dBox.UpdateText("Please select an intersection to build a settlement.");
            Debug.Log("Settlement build mode detected.");
        }
    }

    void checkForRoad(){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.roadBuildingMode){
            dBox.UpdateText("Please select two intersections to build a road.");
        }
    }

    void checkForCity(){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.cityBuildMode){
            dBox.UpdateText("Please select a settlement to build city on.");
            Debug.Log("City build mode detected.");
        }
    }
}
