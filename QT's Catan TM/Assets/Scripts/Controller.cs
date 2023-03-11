using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public List<DevelopmentCard> developmentCards = new List<DevelopmentCard>();
    public List<Player> players = new List<Player>();

    // Class to handle game logic and turns

    public int rollDice(){
        // Create a random number generator
        Random random = new Random();
        // Generate two numbers between 1 and 6 inclusive
        int d1 = random.Next(1,7);
        int d2 = random.Next(1,7);
        return d1 + d2;
    }

    public void showDevelopmentCosts(){
        // Display card showing development card costs.
    }

    public DevelopmentCard buyDevelopmentCard(Player p){
        if(p.resources[ResourceType.Grain] < 1 ||
        p.resources[ResourceType.Ore] < 1 ||
        p.resources[ResourceType.Wool] < 1){
        throw new Exception("Insufficient resources to buy a development card.");
        }

        if(developmentCards.Count == 0){
            throw new Exception("No more development cards available.");
        }

        Random random = new Random();
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
                Console.WriteLine("Player ", p.name, " must give ", numCardstoGive, " cards.");
                // Each player with more than seven resource cards removes half
                // Player can move the robber to anywhere
                rolled.moveRobber();
                // Player who rolled can steal card from whoever has adjacent structure
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
