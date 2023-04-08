using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
     public LineRenderer lineRenderer;

    // Draws a line between two Intersection game objects
    public void drawRoad(Intersection i1, Intersection i2, Player player)
    {
        Dictionary<string, Color> colourMap = new Dictionary<string, Color>{
        {"Red", Color.red},
        {"Blue", Color.blue},
        {"White", Color.white},
        {"Orange", new Color(1f, 0.5f, 0f)}};

        // Get the positions of the two Intersection game objects
        Vector3 pos1 = i1.transform.position;
        Vector3 pos2 = i2.transform.position;

        GameObject road = new GameObject("Road");
        road.transform.SetParent(GameBoard.instance.transform);
        LineRenderer lineRenderer = road.AddComponent<LineRenderer>();

        // Set the positions of the LineRenderer component
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);

        // Enable the LineRenderer component
        lineRenderer.enabled = true;
        

        // Customize the appearance of the line
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = colourMap[player.colour];
        lineRenderer.endColor = colourMap[player.colour];
        lineRenderer.sortingOrder = 100;
    }
}
