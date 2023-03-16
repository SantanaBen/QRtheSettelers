using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour{
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

        void Start()
        {
            
         }

        // Update is called once per frame
        void Update()
        {
        
        }
}