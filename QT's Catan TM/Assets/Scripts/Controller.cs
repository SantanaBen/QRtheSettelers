using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    public List<DevelopmentCard> developmentCards = new List<DevelopmentCard>();
    public List<Player> players = new List<Player>();
    private PlayerQueue gameQueue = new PlayerQueue();
    private bool settlementTriggered = false;
    private bool cityTriggered = false;
    private bool roadTriggered = false;
    public Player currentPlayer;
    public GameBoard board;
    public int numHumanPlayers;
    public string difficulty;
    public int recentRoll;

    public void setRecentRoll(int x){
        recentRoll = x;
    }
    public bool getSettlementTriggered(){
        return settlementTriggered;
    }
    public bool getRoadTriggered(){
        return roadTriggered;
    }
    public void triggerSettlement(){
        settlementTriggered = !settlementTriggered;
    }
    public void triggerRoad(){
        roadTriggered = !roadTriggered;
    }
    
    public void buildSettlement(Player player){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        dBox.UpdateText(player.colour + " has chosen to build a settlement! +1 VP");
        player.victoryPoints++;

        player.resources[ResourceType.Brick]--;
        player.resources[ResourceType.Lumber]--;
        player.resources[ResourceType.Wool]--;
        player.resources[ResourceType.Grain]--;
        
        // Instantiate settlement on place of user input, add to p.settlements
    }

    public void buildRoad(Player player){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        dBox.UpdateText(player.colour + " has chosen to build a road!");

        player.resources[ResourceType.Lumber]--;
        player.resources[ResourceType.Brick]--;
        // Collect user input, two intersections
        // Check if valid for road
        // Instantiate road
        // Add road to player.roads
    }

    public void collectResources(int diceRoll){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        dBox.UpdateText(currentPlayer.colour + " rolled " + recentRoll + "!");
        recentRoll = 0;
        if(diceRoll == 7){
            dBox.UpdateText("Robber activated by " + currentPlayer.colour + "!");
            activateRobber(currentPlayer);
            return;
        }

        List<Tile> boardTiles = board.tiles;
        foreach(Tile tile in boardTiles){
            if(tile.num == diceRoll){
                foreach(Player p in players){
                    p.resources[tile.type]++;
                    Debug.Log("Player given one: " + tile.type);
                }
            }
        }
        // Go through list of tiles
        // Each one with a number matching diceRoll, give each player one of the tile's resource type
    }

    public void checkWinner(){
        foreach(Player p in players){
            if(p.victoryPoints >= 10){
                GameObject box = GameObject.Find("dBox");
                DialogBox dBox = box.GetComponent<DialogBox>();
                dBox.UpdateText(p.colour + " has won the game!.");
                // Go to winning screen. Pass player as param
            }
        }
    }

    public void setUpPlayers(){
        string[] colours = {"Red", "Blue", "White", "Orange"};
        int numCpuPlayers = 4 - numHumanPlayers;
        for(int i = 1; i <= numHumanPlayers; i++){
            Player p = new Player(i.ToString(), false, colours[i-1]);
            players.Add(p);
        }
        for(int i = 1; i <= numCpuPlayers; i++){
            ComputerPlayerAgent p = new ComputerPlayerAgent(i.ToString(), true, colours[i+numHumanPlayers-1]);
            players.Add(p);
        }

        PlayerQueue.PlayerNode p1 = new PlayerQueue.PlayerNode(players[0]);
        PlayerQueue.PlayerNode p2 = new PlayerQueue.PlayerNode(players[1]);
        PlayerQueue.PlayerNode p3 = new PlayerQueue.PlayerNode(players[2]);
        PlayerQueue.PlayerNode p4 = new PlayerQueue.PlayerNode(players[3]);
        p1.setNext(p2);
        p2.setNext(p3);
        p3.setNext(p4);
        p4.setNext(p1);

        gameQueue.current = p1;
        currentPlayer = players[0];
    }

    public void setUpCards(){
        for(int i = 0; i < 5; i++){
            developmentCards.Add(DevelopmentCard.VictoryPoint);
        }
        for(int i = 0; i < 2; i++){
            developmentCards.Add(DevelopmentCard.Monopoly);
            developmentCards.Add(DevelopmentCard.RoadBuilding);
            developmentCards.Add(DevelopmentCard.YearofPlenty);
        }
        for(int i = 0; i < 14; i++){
            developmentCards.Add(DevelopmentCard.Knight);
        }
    }

    public Controller(){
      
    }

    // Class to handle game logic and turns

    public DevelopmentCard buyDevelopmentCard(Player p){
        if(p.resources[ResourceType.Grain] < 1 ||
        p.resources[ResourceType.Ore] < 1 ||
        p.resources[ResourceType.Wool] < 1){
        //("Insufficient resources to buy a development card.");
        }

        if(developmentCards.Count == 0){
            
        }

        System.Random random = new System.Random();
        int index = random.Next(0, developmentCards.Count);
        DevelopmentCard card = developmentCards[index];
        developmentCards.RemoveAt(index);
        developmentCards.Add(card);

        p.resources[ResourceType.Grain] -= 1;
        p.resources[ResourceType.Ore] -= 1;
        p.resources[ResourceType.Wool] -= 1;

        return card;
    }

    public void activateRobber(Player rolled){
        // Called when a player rolls a 7
        // Each player with more than seven resource cards must remove half of their choice
        // Current player chooses a tile to move the robber to
    }

    public void getNextPlayer(){
        gameQueue.nextPlayer();
        currentPlayer = gameQueue.current.getData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkWinner();
    }
}
