using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumBox : MonoBehaviour
{

    public Text sumText;
    private int sumValue;

    // Start is called before the first frame update
    void Start()
    {
        sumText = GetComponent<Text>();
        sumValue = 0;
        sumText.text = sumValue.ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetSum()
    {
        sumValue = 0;
        sumText.text = sumValue.ToString();
    }


    public void UpdateSum(int diceValue){
        sumValue += diceValue;
        sumText.text =  sumValue.ToString();
    }
}
