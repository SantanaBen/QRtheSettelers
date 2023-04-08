using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road{

    Intersection i1;
    Intersection i2;
    Player owner;
        
        public Road(Intersection i1, Intersection i2, Player owner){
            this.i1 = i1;
            this.i2 = i2;
            this.owner = owner;
        }
}