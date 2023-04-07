using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            controller.collectResources(controller.recentRoll);
        }
    }

    void checkForSettlement(){
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.settlementBuildMode){
            Debug.Log("Settlement build mode detected.");
        }
    }

    void checkForRoad(){
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.getRoadTriggered()){

        }
    }

    void checkForCity(){
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.cityBuildMode){
            Debug.Log("City build mode detected.");
        }
    }
}
