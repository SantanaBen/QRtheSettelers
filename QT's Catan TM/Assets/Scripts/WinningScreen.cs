using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinningScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textbox = GameObject.Find("WinningPlayer").GetComponent<TextMeshProUGUI>();
        textbox.text = Controller.winner.colour + " wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
