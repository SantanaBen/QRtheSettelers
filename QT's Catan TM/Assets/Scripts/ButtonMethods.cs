using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonMethods : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void StartGame(){
        GameObject gameControllerObject = new GameObject("GameController");
        Controller gameController = gameControllerObject.AddComponent<Controller>();
        Slider slider = GameObject.Find("PlayersSlider").GetComponent<Slider>();
        gameController.numHumanPlayers = (int)slider.value;
        TextMeshProUGUI textbox = GameObject.Find("DifficultyBox").GetComponent<TextMeshProUGUI>();
        gameController.difficulty = textbox.text;
        gameController.setUpPlayers();
        gameController.setUpCards();

        DontDestroyOnLoad(gameControllerObject);
    }

    public void EndTurn(){
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        dBox.UpdateText(controller.currentPlayer.colour + " has ended their turn.");   
        controller.getNextPlayer();
        TextMeshProUGUI textbox = GameObject.Find("CurrentPlayerBox").GetComponent<TextMeshProUGUI>();
        string playerText;
        if(controller.currentPlayer.cpu){
            playerText = "Player: " + controller.currentPlayer.colour + " [CPU]";
        } else {
            playerText = "Player: " + controller.currentPlayer.colour;
        }
        textbox.text = playerText;
    }

    public void updateText(string message){
        GameObject gameControllerObj = GameObject.Find("dBox");
        DialogBox dBox = gameControllerObj.GetComponent<DialogBox>();
        dBox.UpdateText(message);
    }
   public void Back(){
	 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Forward(){
	 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void GoTo(string sceneName){
         SceneManager.LoadScene(sceneName);
    }

    public void cheat(){
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        p.resources[ResourceType.Brick]+=10;
        p.resources[ResourceType.Lumber]+=10;
        p.resources[ResourceType.Grain]+=10;
        p.resources[ResourceType.Ore]+=10;
        p.resources[ResourceType.Wool]+=10;
    }

    // Called when the buy settlement button is pressed
    public void buySettlementPressed(){
        GameObject box = GameObject.Find("DialogueBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
        (p.resources[ResourceType.Lumber] < 1) ||
        (p.resources[ResourceType.Brick] < 1)  ||
        (p.resources[ResourceType.Grain] < 1)  ||
        (p.resources[ResourceType.Wool] < 1)   
        ){
            dBox.UpdateText("Insufficient resources to build settlement.");
            return;
        } else {
            controller.triggerSettlement();
            GoTo("MainBoard");
        }
    }

    public void buyRoadPressed(){
        GameObject box = GameObject.Find("DialogueBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
            (p.resources[ResourceType.Brick] < 1) ||
            (p.resources[ResourceType.Lumber] < 1)
        ){
            dBox.UpdateText("Insufficient resources to build road.");
            return;
        } else {
            controller.triggerRoad();
            GoTo("MainBoard");
        }

    }

    public void buyCityPressed(){
        GameObject box = GameObject.Find("DialogueBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
            (p.resources[ResourceType.Ore] < 3) ||
            (p.resources[ResourceType.Grain] < 2)
        ){
            // Print to dialogue box not console.
            dBox.UpdateText("Insufficient resources to build city.");
            return;
        } else {
            controller.triggerCity();
            GoTo("MainBoard");
        }
    }

    public void buyDevelopmentCardPressed(){
        GameObject box = GameObject.Find("DialogueBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
            (p.resources[ResourceType.Ore] < 1) ||
            (p.resources[ResourceType.Grain] < 1) ||
            (p.resources[ResourceType.Wool] < 1)
        ){
            // Print to dialogue box not console.
            dBox.UpdateText("Insufficient resources to buy development card.");
            return;
        } else if(controller.developmentCards.Count <= 0){
            dBox.UpdateText("There are no development cards left.");
        } else {
            DevelopmentCard card = controller.buyDevelopmentCard(p);
            dBox.UpdateText("Development card " + card + " purchased!");
        }
    }

}
