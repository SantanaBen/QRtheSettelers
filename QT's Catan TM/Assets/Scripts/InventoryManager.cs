using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryManager : MonoBehaviour {
    
    private Player p;

    // Start is called before the first frame update

    public void displayInventory(Player p){

        TextMeshProUGUI textbox = GameObject.Find("CurrentPlayer").GetComponent<TextMeshProUGUI>();
        if(p.cpu){
            textbox.text = "Player: " +  p.colour + " [CPU]";
        } else {
            textbox.text = "Player: " +  p.colour;
        }
    
        textbox = GameObject.Find("BrickCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Brick: " + p.resources[ResourceType.Brick];

        textbox = GameObject.Find("GrainCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Grain: " + p.resources[ResourceType.Grain];

        textbox = GameObject.Find("WoolCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Wool: " + p.resources[ResourceType.Wool];

        textbox = GameObject.Find("OreCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Ore: " + p.resources[ResourceType.Ore];

        textbox = GameObject.Find("LumberCount").GetComponent<TextMeshProUGUI>();
        textbox.text = "Lumber: " + p.resources[ResourceType.Lumber];

        textbox = GameObject.Find("VictoryPoints").GetComponent<TextMeshProUGUI>();
        textbox.text = "Victory Points: " + p.victoryPoints;

        textbox = GameObject.Find("RoadsBuilt").GetComponent<TextMeshProUGUI>();
        textbox.text = "Roads Built: " + p.roads.Count;

        textbox = GameObject.Find("CitiesBuilt").GetComponent<TextMeshProUGUI>();
        textbox.text = "Cities Built: " + p.cities.Count;

        textbox = GameObject.Find("SettlementsBuilt").GetComponent<TextMeshProUGUI>();
        textbox.text = "Settlements Built: " + p.settlements.Count;

        textbox = GameObject.Find("DevelopmentCards").GetComponent<TextMeshProUGUI>();
        textbox.text = "Development Cards: " + p.developmentCards.Count;
    }

    void Start()
    {
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
        p = controller.currentPlayer;

        displayInventory(p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}