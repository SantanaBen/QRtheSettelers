using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement
{
    public Intersection location;
    public Player owner;

    public Settlement(Intersection location, Player owner){
        this.location = location;
        this.owner = owner;
    }

}
