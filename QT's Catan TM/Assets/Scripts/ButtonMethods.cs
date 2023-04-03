using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonMethods : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        GameObject gameControllerObject = new GameObject("GameController");
        Controller gameController = gameControllerObject.AddComponent<Controller>();
        Slider slider = GameObject.Find("PlayersSlider").GetComponent<Slider>();
        gameController.numHumanPlayers = (int)slider.value;
        TextMeshProUGUI textbox = GameObject.Find("DifficultyBox").GetComponent<TextMeshProUGUI>();
        gameController.difficulty = textbox.text;
        gameController.setUpPlayers();
        DontDestroyOnLoad(gameControllerObject);
    }

   public void Back(){
	 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Forward(){
	 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void GoTo(string sceneName){
         SceneManager.LoadScene(sceneName);
    }
}
