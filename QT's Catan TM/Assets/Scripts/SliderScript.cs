using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public bool isDifficulty;
    public TextMeshProUGUI textbox;

    private string[] difficulties = {"EASY", "MEDIUM", "HARD"};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDifficulty){
            textbox.text = difficulties[(int)slider.value - 1];
        }
        else {
            textbox.text = slider.value.ToString();
        }
    }
}
