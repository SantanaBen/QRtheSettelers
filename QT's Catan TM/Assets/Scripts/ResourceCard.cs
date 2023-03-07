using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceCard {
public ResourceType type;

public ResourceCard(ResourceType type){
        this.type = type;
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

public enum ResourceType
    {
        Brick,
        Lumber,
        Wool,
        Grain,
        Ore
    }