using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{

    public List<DevelopmentCard> developmentCards = new List<DevelopmentCard>();
    public List<Player> players = new List<Player>();
    private PlayerQueue gameQueue = new PlayerQueue();
    public Player currentPlayer;
    public GameBoard board;
    public int numHumanPlayers;
    public string difficulty;
    public int recentRoll;

    public void setRecentRoll(int x){
        recentRoll = x;
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
    public Controller(){
      
    }

    // Class to handle game logic and turns

    public DevelopmentCard buyDevelopmentCard(Player p){
        if(p.resources[ResourceType.Grain] < 1 ||
        p.resources[ResourceType.Ore] < 1 ||
        p.resources[ResourceType.Wool] < 1){
        //throw new Exception("Insufficient resources to buy a development card.");
        }

        if(developmentCards.Count == 0){
            //throw new Exception("No more development cards available.");
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
        foreach(Player p in players){
            int sum = 0;
            foreach(KeyValuePair<ResourceType, int> kvp in p.resources){
                sum += kvp.Value;
            }
            if(sum >= 7){
                decimal x = sum/2;
                x = Math.Floor(x);
                int numCardstoGive = (int)x;
                // Console.WriteLine("Player ", p.name, " must give ", numCardstoGive, " cards.");
                // Each player with more than seven resource cards removes half
                // Player can move the robber to anywhere
                rolled.moveRobber(board);
                // Player who rolled can steal card from whoever has adjacent structure
            }
        }
    }

    public void getNextPlayer(){
        gameQueue.nextPlayer();
        currentPlayer = gameQueue.current.getData();
    }

    public void turn(Player currentPlayer){
        // Turns have three phases: roll, build, trade
    }

    /* public void gameLoop(){
        // Check if game is over
        if(isAbridged){
            // Check timer
        } else {
            foreach(Player p in players){
                if(p.victoryPoints <= 10){
                  // Game has been won.
                }
            }
        }
    } */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
