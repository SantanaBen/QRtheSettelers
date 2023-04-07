using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameBoard.instance.gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
