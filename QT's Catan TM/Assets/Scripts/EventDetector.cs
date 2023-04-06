using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EventDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject boardObject =  GameObject.Find("GameBoard");
        DontDestroyOnLoad(boardObject);

        checkForDiceRoll();
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        TextMeshProUGUI textbox = GameObject.Find("CurrentPlayerBox").GetComponent<TextMeshProUGUI>();
        textbox.text = "Player: " + controller.currentPlayer.colour;
    
    }

    void checkForDiceRoll(){
        GameObject controllerBox =  GameObject.Find("GameController");
        Controller controller =  controllerBox.GetComponent<Controller>();
        if(controller.recentRoll != 0){
            controller.collectResources(controller.recentRoll);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
