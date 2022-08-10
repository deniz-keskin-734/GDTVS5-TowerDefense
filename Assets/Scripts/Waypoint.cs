using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool towerPlacable;
    [SerializeField] TowerScript tower;

    private void OnMouseDown()
    {
        if (towerPlacable)
        {
            bool isPlaced = tower.CreateTower(tower, transform.position);
            towerPlacable = !isPlaced;
        }
    }
}
