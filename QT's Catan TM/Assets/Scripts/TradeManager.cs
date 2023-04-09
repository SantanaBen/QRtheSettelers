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
    Player traderA;
    Player traderB;

    public List<Slider> currentSliders;
    public List<Slider> otherSliders;
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

        traderA = p;
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
        traderB = p1;
        loadOther(p1);
    }
    public void p2Pressed(){
        traderB = p2;
        loadOther(p2);
    }
    public void p3Pressed(){
        traderB = p3;
        loadOther(p3);
    }

    public void rejectTrade(){
        for(int i = 0; i < 5; i++){
            currentSliders[i].value = 0;
            otherSliders[i].value = 0;
        }
        GameObject.Find("Player1").GetComponent<Button>().interactable = true;
        GameObject.Find("Player2").GetComponent<Button>().interactable = true;
        GameObject.Find("Player3").GetComponent<Button>().interactable = true;
        GameObject.Find("AcceptTrade").GetComponent<Button>().interactable = false;
        GameObject.Find("RejectTrade").GetComponent<Button>().interactable = false;

        GameObject.Find("TradeDialogue").GetComponent<TextMeshProUGUI>().text = "Trade rejected.";
    }

    public void acceptTrade(){
        GameObject.Find("TradeDialogue").GetComponent<TextMeshProUGUI>().text = "Trade accepted";
        List<ResourceType> tradeMaterials = new List<ResourceType>(){ResourceType.Ore, ResourceType.Lumber, ResourceType.Brick, ResourceType.Wool, ResourceType.Grain}; 
        for(int i = 0; i < 5; i++){
                traderA.resources[tradeMaterials[i]] -= (int)currentSliders[i].value;
                traderB.resources[tradeMaterials[i]] += (int)currentSliders[i].value;

                traderA.resources[tradeMaterials[i]] += (int)otherSliders[i].value;
                traderB.resources[tradeMaterials[i]] -= (int)otherSliders[i].value;
        }

        loadCurrent(traderA);
        loadOther(traderB);
    }


    public void proposeTrade(){
        int[] a = new int[5];
        int[] b = new int[5];

        for(int i = 0; i < 5; i++){
            a[i] = (int)currentSliders[i].value;
            b[i] = (int)otherSliders[i].value;
        }

        if(checkTrade(a, b)){
            GameObject.Find("TradeDialogue").GetComponent<TextMeshProUGUI>().text = "Trade proposed. Accept or Reject?";
            GameObject.Find("AcceptTrade").GetComponent<Button>().interactable = true;
            GameObject.Find("RejectTrade").GetComponent<Button>().interactable = true;
            GameObject.Find("Player1").GetComponent<Button>().interactable = false;
            GameObject.Find("Player2").GetComponent<Button>().interactable = false;
            GameObject.Find("Player3").GetComponent<Button>().interactable = false;

            if(traderB.cpu){
                GameObject.Find("AcceptTrade").GetComponent<Button>().interactable = false;
                GameObject.Find("RejectTrade").GetComponent<Button>().interactable = false;
                int choice = UnityEngine.Random.Range(1,3);
                if(choice == 1){
                    Invoke("acceptTrade", 2.0f);
                } else {
                    Invoke("rejectTrade", 2.0f);
                }
            }

        } else {
            for(int i = 0; i < 5; i++){
                currentSliders[i].value = 0;
                otherSliders[i].value = 0;
            }
            GameObject.Find("TradeDialogue").GetComponent<TextMeshProUGUI>().text = "Invalid trade.";
        }
    }

    public bool checkTrade(int[] a, int[] b){
        bool currentNull = true;
        bool otherNull = true;
        for(int i = 0; i < 5; i++){
            if(a[i] != 0){
                currentNull = false;
            }
            if(b[i] != 0){
                otherNull = false;
            }
        }
        return !(currentNull || otherNull);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
