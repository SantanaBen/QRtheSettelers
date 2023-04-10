using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Controller : MonoBehaviour
{

    public List<DevelopmentCard> developmentCards = new List<DevelopmentCard>();
    public List<Player> players = new List<Player>();
    private PlayerQueue gameQueue = new PlayerQueue();
    private bool settlementTriggered = false;
    private bool cityTriggered = false;
    private bool roadTriggered = false;
    public Player currentPlayer;
    public int numHumanPlayers;
    public string difficulty;
    public int recentRoll;
    public bool settlementBuildMode = false;
    public bool cityBuildMode = false;
    public bool roadBuildingMode = false;
    public static Player winner = null;
    public bool robberMode = false;

    public void setRecentRoll(int x){
        recentRoll = x;
    }

    public bool verifySettlementLocation(Intersection i1){
        Debug.Log("Verifying settlement location.");
        Debug.Log("Settlement present: " + i1.settlementPresent);
        return(!i1.settlementPresent);
    }

    public bool verifyCityLocation(Intersection i1, Player p){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        if(!i1.settlementPresent){
            dBox.UpdateText("City cannot be built. There is no settlement here.");
            return false;
        }
        if(i1.settlement.owner != p){
            dBox.UpdateText("City cannot be built. Settlement does not belong to player.");
            return false;
        }
        return true;
    }

    public bool verifyRoadLocation(Intersection i1, Intersection i2){
        if(i1.roadPresent && i2.roadPresent){
            return false;
        }
        List<Tile> l1 = new List<Tile>();
        l1.Add(i1.t1);
        l1.Add(i1.t2);
        l1.Add(i1.t3);

        List<Tile> l2 = new List<Tile>();
        l2.Add(i2.t1);
        l2.Add(i2.t2);
        l2.Add(i2.t3);

        int count = 0;
        // Count incremented when intersections share common tile

        foreach(Tile t1 in l1){
            foreach(Tile t2 in l2){
                if(t1 == t2){
                    count++;
                }
            }
        }

        // Road can only be built between two intersections if they share two tiles.
        return(count == 2);
    }
    
    public void buildSettlement(Player player, Intersection i1){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();

        if(!verifySettlementLocation(i1)){
            dBox.UpdateText("Invalid settlement location.");
            settlementBuildMode = false;
            return;
        }

        Dictionary<string, Color> colourMap = new Dictionary<string, Color>{
        {"red", Color.red},
        {"blue", Color.blue},
        {"white", Color.white},
        {"orange", new Color(1f, 0.5f, 0f)}};

        dBox.UpdateText(player.colour + " has chosen to build a settlement! +1 VP");
        player.victoryPoints++;

        player.resources[ResourceType.Brick]--;
        player.resources[ResourceType.Lumber]--;
        player.resources[ResourceType.Wool]--;
        player.resources[ResourceType.Grain]--;

        Settlement s1 = new Settlement(i1, player);
        player.settlements.Add(s1);
        i1.settlement = s1;
        SpriteRenderer renderer = i1.gameObject.GetComponent<SpriteRenderer>();
        renderer.color = colourMap[player.colour.ToLower()];
        i1.settlementPresent = true;
        settlementBuildMode = false;
    }

    public void buildCity(Player player, Intersection i1){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();

        if(!verifyCityLocation(i1, player)){
            cityBuildMode = false;
            return;
        }

        dBox.UpdateText(player.colour + " has chosen to build a city! +2 VP");
        player.victoryPoints += 2;

        player.resources[ResourceType.Ore] -=3;
        player.resources[ResourceType.Grain] -=2;

        Transform transform = i1.gameObject.transform;
        transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
        City city = new City(i1, player);
        i1.cityPresent = true;
        player.cities.Add(city);
        cityBuildMode = false;
    }

    public void buildRoad(Player player, Intersection i1, Intersection i2){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();

        if(!verifyRoadLocation(i1,i2)){
            dBox.UpdateText("Invalid road locations. Pick two adjacent intersections");
            roadBuildingMode = false;
            return;
        }
        dBox.UpdateText(player.colour + " has chosen to build a road!");
        GameObject rb = GameObject.Find("RoadBuilder");
        RoadBuilder roadBuild = rb.GetComponent<RoadBuilder>();
        roadBuild.drawRoad(i1, i2, player);

        player.resources[ResourceType.Lumber]--;
        player.resources[ResourceType.Brick]--;
        Road road = new Road(i1, i2, player); 
        player.roads.Add(road);
        i1.roadPresent = true;
        i2.roadPresent = true;
        i1.roads.Add(road);
        i2.roads.Add(road);
        roadBuildingMode = false;
    }

    public void collectResources(int diceRoll){
        GameObject box = GameObject.Find("dBox");
        DialogBox dBox = box.GetComponent<DialogBox>();
        dBox.UpdateText(currentPlayer.colour + " rolled " + diceRoll + "!");
        recentRoll = 0;
        if(diceRoll == 7){
            robberMode = true;
            dBox.UpdateText("Select a tile to move the robber to: ");
            if(currentPlayer.cpu){
                Invoke("aiMoveRobber", 1.0f);
            }
            takeHalf();
            return;
        }
     
        List<Tile> boardTiles = GameBoard.instance.tiles;
        List<Intersection> boardIntersections = GameBoard.instance.intersections;
        foreach(Tile tile in boardTiles){
            if(tile.num == diceRoll){
                if(!tile.getRobberPresent()){
                    foreach(Intersection i in boardIntersections){
                        if(i.t1 == tile || i.t2 == tile || i.t3 == tile){
                            if(i.settlementPresent){
                                dBox.UpdateText(diceRoll + " rolled. " + i.settlement.owner.colour + " given 1 " + tile.type);
                                i.settlement.owner.resources[tile.type]++;
                            }
                        }
                    }
                }
            }
        }
    }

    public void takeHalf(){
        List<Player> toTake = new List<Player>();
        foreach(Player player in players){
            List<int> valuesList = player.resources.Values.ToList();
            if(valuesList.Sum() > 7){
                toTake.Add(player);
            }
        }
        List<ResourceType> types = new List<ResourceType>(players[0].resources.Keys); 
        foreach(Player p in toTake){
            int total = p.resources.Values.ToList().Sum();
            int halfResourceCount = Mathf.FloorToInt(total / 2);
            while(p.resources.Values.ToList().Sum() > halfResourceCount){
                ResourceType randomResource = types[UnityEngine.Random.Range(0,5)];
                if(p.resources[randomResource] > 0){
                    p.resources[randomResource]--;
                }
            }
        }
    }

    public void aiMoveRobber(){
        List<Tile> tileList = GameBoard.instance.tiles;
        Tile chosenTile = null;

        while(chosenTile == null){
            int randomIndex = UnityEngine.Random.Range(0, tileList.Count);
            Tile randomTile = tileList[randomIndex];
            if(!randomTile.getRobberPresent()){
                chosenTile = randomTile;
            }
        }
        activateRobber(currentPlayer, chosenTile);
    }

    public bool checkWinner(){
        foreach(Player p in players){
            if(p.victoryPoints >= 10){
                return true;
            }
        }
        return false;
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

    public bool getSettlementTriggered(){
        return settlementTriggered;
    }
    public bool getCityTriggered(){
        return cityTriggered;
    }
    public bool getRoadTriggered(){
        return roadTriggered;
    }
    public void triggerSettlement(){
        settlementBuildMode = !settlementBuildMode;
    }
    public void triggerRoad(){
        roadBuildingMode = !roadBuildingMode;
    }
    public void triggerCity(){
        cityBuildMode = !cityBuildMode;
    }

    public DevelopmentCard buyDevelopmentCard(Player p){
        
        int index = UnityEngine.Random.Range(0, developmentCards.Count);
        DevelopmentCard card = developmentCards[index];

        developmentCards.RemoveAt(index);
        p.developmentCards.Add(card);

        p.resources[ResourceType.Grain] -= 1;
        p.resources[ResourceType.Ore] -= 1;
        p.resources[ResourceType.Wool] -= 1;

        return card;
    }

    public void activateRobber(Player rolled, Tile tile){
        GameBoard.instance.currentRobberTile.toggleRobber();
        tile.toggleRobber();
        GameBoard.instance.currentRobberTile = tile;

        GameObject robberPawn = GameObject.Find("RobberPawn");
        Vector3 robberPosition = robberPawn.transform.position;
        robberPawn.transform.position = tile.transform.position;

        // If players have more than 7 resources, take a random half
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
        if(checkWinner()){
            SceneManager.LoadScene("WinningScreen");
            winner = currentPlayer;
            Destroy(GameBoard.instance.gameObject);
            Destroy(gameObject);
        }
        if(SceneManager.GetActiveScene().name == "MainBoard"){
        if((currentPlayer.cpu)){
            //Disable buttons if CPU turn
            GameObject.Find("RollButton").GetComponent<Button>().interactable = false;
            GameObject.Find("BuildingCostsButton").GetComponent<Button>().interactable = false;
            GameObject.Find("BuyButton").GetComponent<Button>().interactable = false;
            GameObject.Find("TradeButton").GetComponent<Button>().interactable = false;
            GameObject.Find("Inventory").GetComponent<Button>().interactable = false;
            GameObject.Find("EndTurn").GetComponent<Button>().interactable = false;
        } else {
            GameObject.Find("BuildingCostsButton").GetComponent<Button>().interactable = true;
            GameObject.Find("BuyButton").GetComponent<Button>().interactable = true;
            GameObject.Find("TradeButton").GetComponent<Button>().interactable = true;
            GameObject.Find("Inventory").GetComponent<Button>().interactable = true;
            GameObject.Find("EndTurn").GetComponent<Button>().interactable = true;
            }   
        }
    }
}
