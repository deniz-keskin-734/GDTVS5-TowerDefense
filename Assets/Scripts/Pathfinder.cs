using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Vector2Int startCoordinates;
    public Vector2Int destinationCoordinates;

    Node startNode, destinationNode, currentSearchNode;
    Dictionary<Vector2, Node> reachedNodes = new Dictionary<Vector2, Node>();
    Queue<Node> searchQueue = new Queue<Node>();
    Vector2Int[] searchDirections = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];
    }
    private void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(startCoordinates); //pg49
        return BuildPath();
    }
    public List<Node> GetNewPath(Vector2Int coordinates) //pg59
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates); 
        return BuildPath();
    }
    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        searchQueue.Clear();
        reachedNodes.Clear();

        bool isRunning = true;
        searchQueue.Enqueue(gridManager.Grid[coordinates]);
        reachedNodes.Add(coordinates, gridManager.Grid[coordinates]);

        while(searchQueue.Count > 0 && isRunning)
        {
            currentSearchNode = searchQueue.Dequeue();
            FindNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private void FindNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in searchDirections)
        {
            if(gridManager.Grid.ContainsKey(currentSearchNode.coordinates + direction))
            {
                neighbors.Add(gridManager.Grid[currentSearchNode.coordinates + direction]);
            }
        }
        foreach (Node neighbor in neighbors)
        {
            if(!reachedNodes.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reachedNodes.Add(neighbor.coordinates, neighbor);
                searchQueue.Enqueue(neighbor);
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }

    public bool PathBlockageCheck(Vector2Int coordinates) //pg53
    {
        if (gridManager.Grid.ContainsKey(coordinates))
        {
            bool initialState = gridManager.Grid[coordinates].isWalkable;
            gridManager.Grid[coordinates].isWalkable = false;
            var tempCheckPath = GetNewPath();
            gridManager.Grid[coordinates].isWalkable = initialState;

            if(tempCheckPath.Count <= 1) 
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("FindPath", false, SendMessageOptions.DontRequireReceiver);
    }
}
