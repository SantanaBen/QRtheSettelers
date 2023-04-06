using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject board = GameObject.Find("GameBoard");
        board.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
