using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Node 
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
