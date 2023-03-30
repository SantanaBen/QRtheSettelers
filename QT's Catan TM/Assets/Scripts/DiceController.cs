using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
     public int sum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void TestButtonMethod()
    {

          SumBox sumBox = GameObject.Find("SumBox").GetComponent<SumBox>();
          sumBox.ResetSum();
         GameObject dice1 = GameObject.Find("Dice1"); // Find the object with the name "Dice1"
         Dice diceScript1 = dice1.GetComponent<Dice>();        
         diceScript1.OnMouseDown();

         GameObject dice2 = GameObject.Find("Dice2"); // Find the object with the name "Dice2"
         Dice diceScript2 = dice2.GetComponent<Dice>();        
         diceScript2.OnMouseDown();
         
         int dice1Value = diceScript1.faceValue;
         int dice2Value = diceScript2.faceValue;

         int sum = dice1Value + dice2Value;
         Debug.Log("Sum of dice values: " + sum);

    }
}
