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
        textbox.text = "Player: " + controller.currentPlayer.colour;
    }

    public void updateText(){
        string message = "This is a new message.";
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

    // Called when the buy settlement button is pressed
    public void buySettlementPressed(){
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
        (p.resources[ResourceType.Lumber] < 1) ||
        (p.resources[ResourceType.Brick] < 1)  ||
        (p.resources[ResourceType.Grain] < 1)  ||
        (p.resources[ResourceType.Wool] < 1)   ){
            // Print to dialogue box, not console!
            Debug.Log("Insufficient funds to build settlement.");
            return;
        } else {
            controller.triggerSettlement();
            GoTo("MainBoard");
        }
    }

    public void buyRoadPressed(){
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        if(
            (p.resources[ResourceType.Brick] < 1) ||
            (p.resources[ResourceType.Lumber] < 1)
        ){
            // Print to dialogue box not console.
            Debug.Log("Insufficient funds to build road.");
            return;
        } else {
            controller.triggerRoad();
            GoTo("MainBoard");
        }

    }

    public void buyCityPressed(){

    }

    public void buyDevelopmentCardPressed(){
        
    }

}
