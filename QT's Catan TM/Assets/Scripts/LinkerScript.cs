using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObj = GameObject.Find("GameController");
        Controller controller = gameControllerObj.GetComponent<Controller>();
      

        GameBoard gameBoard = GameObject.Find("GameBoard").GetComponent<GameBoard>(); 
        //Debug.Log(gameBoard.tiles.Count +  " tiles currently in board.");

        controller.board = gameBoard;
        //Debug.Log("Linked controller to board");

        //Debug.Log(controller.board.tiles.Count + " tiles in controller's board.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
