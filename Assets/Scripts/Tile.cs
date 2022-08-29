using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool towerPlacable;
    [SerializeField] TowerScript tower;

    GridManager grid;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z);
        if(!towerPlacable)
        {
            grid.BlockNode(coordinates);
        }
    }

    private void OnMouseDown()
    {
        if (grid.GetNode(coordinates).isWalkable && !pathfinder.PathBlockageCheck(coordinates))
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            if (isPlaced)
            {
                grid.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
