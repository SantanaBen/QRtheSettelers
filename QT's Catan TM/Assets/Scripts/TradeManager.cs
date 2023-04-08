using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TradeManager : MonoBehaviour
{

    // Start is called before the first frame update

    Player p1;
    Player p2;
    Player p3;
    void Start()
    {
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        Player p = controller.currentPlayer;
        List<Player> traders = new List<Player>();
        foreach(Player player in controller.players){
            if(player != p){
                traders.Add(player);
            }
        }
        p1 = traders[0];
        p2 = traders[1];
        p3 = traders[2];

        loadCurrent(p);
    }

    public void loadCurrent(Player player){
        TextMeshProUGUI textbox = GameObject.Find("CurrentPlayerName").GetComponent<TextMeshProUGUI>();
        textbox.text = player.colour + "'s Inventory";
        textbox = GameObject.Find("CurrentGrainCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Grain: " + player.resources[ResourceType.Grain];
        textbox = GameObject.Find("CurrentOreCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Ore: " + player.resources[ResourceType.Ore];
        textbox = GameObject.Find("CurrentWoolCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Wool: " + player.resources[ResourceType.Wool];
        textbox = GameObject.Find("CurrentLumberCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Lumber: " + player.resources[ResourceType.Lumber];
        textbox = GameObject.Find("CurrentBrickCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Brick: " + player.resources[ResourceType.Brick];

        GameObject.Find("Player1").GetComponentInChildren<TextMeshProUGUI>().text = p1.colour;
        GameObject.Find("Player2").GetComponentInChildren<TextMeshProUGUI>().text = p2.colour;
        GameObject.Find("Player3").GetComponentInChildren<TextMeshProUGUI>().text = p3.colour;
        updateCurrentSliders(player);

    }

    public void updateCurrentSliders(Player player){
        Slider slider = GameObject.Find("CurrentGrainSlider").GetComponent<Slider>();
        slider.maxValue = (float)player.resources[ResourceType.Grain];
        slider = GameObject.Find("CurrentOreSlider").GetComponent<Slider>();
        slider.maxValue = (float)player.resources[ResourceType.Ore];
        slider = GameObject.Find("CurrentWoolSlider").GetComponent<Slider>();
        slider.maxValue = (float)player.resources[ResourceType.Wool];
        slider = GameObject.Find("CurrentLumberSlider").GetComponent<Slider>();
        slider.maxValue = (float)player.resources[ResourceType.Lumber];
        slider = GameObject.Find("CurrentBrickSlider").GetComponent<Slider>();
        slider.maxValue = (float)player.resources[ResourceType.Brick];


    }

    public void loadOther(Player other){
        TextMeshProUGUI textbox = GameObject.Find("ChosenPlayerName").GetComponent<TextMeshProUGUI>();
        textbox.text = other.colour + "'s Inventory";
        textbox = GameObject.Find("ChosenGrainCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Grain: " + other.resources[ResourceType.Grain];
        textbox = GameObject.Find("ChosenOreCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Ore: " + other.resources[ResourceType.Ore];
        textbox = GameObject.Find("ChosenWoolCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Wool: " + other.resources[ResourceType.Wool];
        textbox = GameObject.Find("ChosenLumberCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Lumber: " + other.resources[ResourceType.Lumber];
        textbox = GameObject.Find("ChosenBrickCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Brick: " + other.resources[ResourceType.Brick];
        updateOtherSliders(other);
    }

    public void updateOtherSliders(Player other){
        Slider slider = GameObject.Find("ChosenGrainSlider").GetComponent<Slider>();
        slider.maxValue = (float)other.resources[ResourceType.Grain];
        slider = GameObject.Find("ChosenOreSlider").GetComponent<Slider>();
        slider.maxValue = (float)other.resources[ResourceType.Ore];
        slider = GameObject.Find("ChosenWoolSlider").GetComponent<Slider>();
        slider.maxValue = (float)other.resources[ResourceType.Wool];
        slider = GameObject.Find("ChosenLumberSlider").GetComponent<Slider>();
        slider.maxValue = (float)other.resources[ResourceType.Lumber];
        slider = GameObject.Find("ChosenBrickSlider").GetComponent<Slider>();
        slider.maxValue = (float)other.resources[ResourceType.Brick];
    }

    public void p1Pressed(){
        loadOther(p1);
    }
    public void p2Pressed(){
        loadOther(p2);
    }
    public void p3Pressed(){
        loadOther(p3);
    }

    public void proposeTrade(){
        if(checkTrade()){
            // Set buttons as interactable
        } else {
            // Set sliders to 0
            // Dialogue box, invalid trade.
        }
    }

    public bool checkTrade(){
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
