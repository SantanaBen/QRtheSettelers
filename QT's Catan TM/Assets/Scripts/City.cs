using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{

    public Intersection location;
    public Player owner;

    public City(Intersection location, Player owner){
        this.location = location;
        this.owner = owner;
    }
}
