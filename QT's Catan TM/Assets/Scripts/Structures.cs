using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour
{

    public class Road{
        List<RoadSegment> roadPath;
        int length;
        public Road(){
            length = 0;
            roadPath = new List<RoadSegment>();
        }
        public void addRoad(RoadSegment road){
            roadPath.Add(road);
            length++;
            // Add victory points to player
        }
        
        public class RoadSegment{
            // Roads can be joint together, each segment is placed at the intersection of two tiles.
            Tile locationA;
            Tile locationB;
        }
    }


    public class Settlement{
        private Tile location;
    }

    public class City{
        private Tile location;
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
